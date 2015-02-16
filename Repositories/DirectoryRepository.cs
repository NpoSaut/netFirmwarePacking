using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Представляет репозиторий, хранилище которого расположено в папке с файлами</summary>
    public class DirectoryRepository : Repository
    {
        private readonly ConcurrentDictionary<String, IRepositoryElement> _elementsCache;

        /// <summary>Создаёт репозиторий по указанному пути</summary>
        /// <param name="RepositoryRootPath">Пусть к репозиторию</param>
        public DirectoryRepository(String RepositoryRootPath)
            : this(new DirectoryInfo(RepositoryRootPath)) { }

        /// <summary>Создаёт репозиторий в указанной директори</summary>
        /// <param name="RepositoryRoot">Директория с репозиторием</param>
        public DirectoryRepository(DirectoryInfo RepositoryRoot)
        {
            this.RepositoryRoot = RepositoryRoot;
            _elementsCache = new ConcurrentDictionary<string, IRepositoryElement>();
        }

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

        /// <summary>Папка расположения репозитория</summary>
        public DirectoryInfo RepositoryRoot { get; private set; }

        public override IEnumerable<IRepositoryElement> Packages
        {
            get
            {
                return !RepositoryRoot.Exists
                           ? Enumerable.Empty<IRepositoryElement>()
                           : RepositoryRoot.EnumerateFiles("*." + FirmwarePackage.FirmwarePackageExtension, SearchOption.AllDirectories)
                                           .Select(LoadPackage);
            }
        }

        private IRepositoryElement LoadPackage(FileInfo File)
        {
            IRepositoryElement result;
            if (!_elementsCache.TryGetValue(File.FullName, out result))
            {
                result = new MemoryRepositoryElement(FirmwarePackage.Open(File));
                _elementsCache.TryAdd(File.FullName, result);
            }
            return result;
        }
    }
}
