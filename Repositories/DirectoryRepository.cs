using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Представляет репозиторий, хранилище которого расположено в папке с файлами</summary>
    public class DirectoryRepository : Repository
    {
        private readonly List<FileLinkRepositoryElement> _packages;

        /// <summary>Создаёт репозиторий по указанному пути</summary>
        /// <param name="RepositoryRootPath">Пусть к репозиторию</param>
        public DirectoryRepository(String RepositoryRootPath)
            : this(new DirectoryInfo(RepositoryRootPath)) { }

        /// <summary>Создаёт репозиторий в указанной директори</summary>
        /// <param name="RepositoryRoot">Директория с репозиторием</param>
        public DirectoryRepository(DirectoryInfo RepositoryRoot)
        {
            this.RepositoryRoot = RepositoryRoot;

            _packages =
                RepositoryRoot.Exists
                    ? RepositoryRoot.EnumerateFiles("*." + FirmwarePackage.FirmwarePackageExtension)
                                    .Select(FileLinkRepositoryElement.Load)
                                    .ToList()
                    : new List<FileLinkRepositoryElement>();
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
            get { return _packages; }
        }
    }
}
