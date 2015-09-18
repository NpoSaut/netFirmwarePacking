using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FirmwarePacking.Repositories
{
    public class NotifyDirectoryRepository : DirectoryRepository, INotifyRepository
    {
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _fileWatchOperations =
            new ConcurrentDictionary<string, CancellationTokenSource>();

        private readonly object _locker = new object();
        private readonly FileSystemWatcher _watcher;

        /// <summary>Создаёт репозиторий по указанному пути</summary>
        /// <param name="RepositoryRootPath">Пусть к репозиторию</param>
        public NotifyDirectoryRepository(string RepositoryRootPath) : base(RepositoryRootPath)
        {
            _watcher = new FileSystemWatcher(RepositoryRootPath, PackageSearchPattern) { IncludeSubdirectories = true };
        }

        public event EventHandler<RepositoryUpdatedEventArgs> Updated;

        public void Dispose() { _watcher.Dispose(); }

        protected override void UnderlockedInitialize()
        {
            base.UnderlockedInitialize();

            _watcher.Created += WatcherOnCreated;
            _watcher.Deleted += WatcherOnDeleted;
            _watcher.Renamed += WatcherOnRenamed;
            _watcher.EnableRaisingEvents = true;
        }

        private void WatcherOnCreated(object Sender, FileSystemEventArgs e) { ProcessFileCreated(e.FullPath); }
        private void WatcherOnDeleted(object Sender, FileSystemEventArgs e) { ProcessFileRemoved(e.FullPath); }

        private void WatcherOnRenamed(object Sender, RenamedEventArgs e)
        {
            if (e.OldFullPath != null
                && Path.GetExtension(e.OldFullPath).Equals("." + FirmwarePackage.FirmwarePackageExtension, StringComparison.CurrentCultureIgnoreCase))
                ProcessFileRemoved(e.OldFullPath);
            if (e.FullPath != null
                && Path.GetExtension(e.FullPath).Equals("." + FirmwarePackage.FirmwarePackageExtension, StringComparison.CurrentCultureIgnoreCase))
                ProcessFileCreated(e.FullPath);
        }

        private void ProcessFileCreated(string FileName)
        {
            var ts = new CancellationTokenSource();
            _fileWatchOperations.TryAdd(FileName, ts);
            CancellationToken token = ts.Token;
            Task.Factory.StartNew(() =>
                                  {
                                      try
                                      {
                                          SpinWait.SpinUntil(() =>
                                                             {
                                                                 token.ThrowIfCancellationRequested();
                                                                 return TestFile(FileName);
                                                             });
                                          IRepositoryElement element = LoadElement(FileName);
                                          lock (_locker)
                                          {
                                              ElementsCache.Add(FileName, element);
                                              PackagesCollection.Add(element);
                                          }
                                          OnUpdated(RepositoryUpdatedEventArgs.CreateAddedEventArgs(element));
                                      }
                                      catch (OperationCanceledException e) { }
                                      finally
                                      {
                                          _fileWatchOperations.TryRemove(FileName, out ts);
                                      }
                                  }, token);
        }

        private void ProcessFileRemoved(string FullPath)
        {
            CancellationTokenSource ts;
            if (_fileWatchOperations.TryRemove(FullPath, out ts))
                ts.Cancel();
            IRepositoryElement element;
            lock (_locker)
            {
                if (ElementsCache.TryGetValue(FullPath, out element))
                {
                    ElementsCache.Remove(FullPath);
                    PackagesCollection.Remove(element);
                }
            }
            if (element != null)
                OnUpdated(RepositoryUpdatedEventArgs.CreateRemovedEventArgs(element));
        }

        private bool TestFile(String FileName)
        {
            try
            {
                using (File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual void OnUpdated(RepositoryUpdatedEventArgs E)
        {
            EventHandler<RepositoryUpdatedEventArgs> handler = Updated;
            if (handler != null) handler(this, E);
        }
    }
}
