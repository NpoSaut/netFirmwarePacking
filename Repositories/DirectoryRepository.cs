using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Представляет репозиторий, хранилище которого расположено в папке с файлами</summary>
    public class DirectoryRepository : RepositoryBase
    {
        protected const string PackageSearchPattern = "*." + FirmwarePackage.FirmwarePackageExtension;

        private readonly object _initializationLocker = new object();

        protected IDictionary<String, IRepositoryElement> ElementsCache;
        protected IList<IRepositoryElement> PackagesCollection;
        private bool _initialized;
        private ICollection<IRepositoryElement> _packagesRoc;

        /// <summary>Создаёт репозиторий по указанному пути</summary>
        /// <param name="RepositoryRootPath">Пусть к репозиторию</param>
        public DirectoryRepository(String RepositoryRootPath)
        {
            RepositoryRoot = new DirectoryInfo(RepositoryRootPath);
            if (!RepositoryRoot.Exists)
                RepositoryRoot.Create();
        }

        /// <summary>Папка расположения репозитория</summary>
        public DirectoryInfo RepositoryRoot { get; private set; }

        public override ICollection<IRepositoryElement> Packages
        {
            get
            {
                if (!_initialized)
                    Initialize();
                return _packagesRoc;
            }
        }

        protected void Initialize()
        {
            lock (_initializationLocker)
            {
                if (_initialized) return;
                UnderlockedInitialize();
            }
        }

        protected virtual void UnderlockedInitialize()
        {
            ElementsCache = LoadPackages();
            PackagesCollection = ElementsCache.Values.ToList();
            _packagesRoc = new ReadOnlyCollection<IRepositoryElement>(PackagesCollection);
            _initialized = true;
        }

        private IDictionary<string, IRepositoryElement> LoadPackages()
        {
            var res = new Dictionary<string, IRepositoryElement>();

            foreach (FileInfo file in RepositoryRoot.EnumerateFiles(PackageSearchPattern, SearchOption.AllDirectories))
            {
                try
                {
                    res.Add(file.FullName, LoadElement(file.FullName));
                }
                catch (Exception) { }
            }

            return res;
        }

        protected IRepositoryElement LoadElement(String FileName) { return new MemoryRepositoryElement(FirmwarePackage.Open(FileName)); }

        #region Static Fileds

        /// <summary>Путь к папке с пользовательскими репозиториями</summary>
        public static String UserRepositoryDirectory
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Firmwares"); }
        }

        /// <summary>Путь к папке с репозиторием приложения</summary>
        public static String ApplicationRepositoryDirectory
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository"); }
        }

        #endregion
    }
}
