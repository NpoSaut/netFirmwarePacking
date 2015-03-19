using System;
using System.IO;

namespace FirmwarePacking.Repositories
{
    public class NotifyDirectoryRepository : DirectoryRepository, INotifyRepository
    {
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

        private void WatcherOnRenamed(object Sender, RenamedEventArgs e)
        {
            lock (_locker)
            {
                IRepositoryElement element;
                if (ElementsCache.TryGetValue(e.OldFullPath, out element))
                {
                    ElementsCache.Add(e.FullPath, element);
                    ElementsCache.Remove(e.OldFullPath);
                }
            }
        }

        private void WatcherOnCreated(object Sender, FileSystemEventArgs e)
        {
            IRepositoryElement element = LoadElement(e.FullPath);
            lock (_locker)
            {
                ElementsCache.Add(e.FullPath, element);
                PackagesCollection.Add(element);
            }
            OnUpdated(RepositoryUpdatedEventArgs.CreateAddedEventArgs(element));
        }

        private void WatcherOnDeleted(object Sender, FileSystemEventArgs e)
        {
            IRepositoryElement element;
            lock (_locker)
            {
                if (ElementsCache.TryGetValue(e.FullPath, out element))
                {
                    ElementsCache.Remove(e.FullPath);
                    PackagesCollection.Remove(element);
                }
            }
            if (element != null)
                OnUpdated(RepositoryUpdatedEventArgs.CreateRemovedEventArgs(element));
        }

        protected virtual void OnUpdated(RepositoryUpdatedEventArgs E)
        {
            EventHandler<RepositoryUpdatedEventArgs> handler = Updated;
            if (handler != null) handler(this, E);
        }
    }
}
