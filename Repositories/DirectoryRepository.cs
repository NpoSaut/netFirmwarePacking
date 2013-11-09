using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwarePacking.Repositories
{
    /// <summary>
    /// Представляет репозиторий, хранилище которого расположено в папке с файлами
    /// </summary>
    public class DirectoryRepository : Repository
    {
        /// <summary>
        /// Путь к папке с пользовательскими репозиториями
        /// </summary>
        public static String UserRepositoryDirectory
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Firmwares"); }
        }
        /// <summary>
        /// Путь к папке с репозиторием приложения
        /// </summary>
        public static String ApplicatoinRepositoryDirectory
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository"); }
        }

        /// <summary>Папка расположения репозитория</summary>
        public DirectoryInfo RepositoryRoot { get; private set; }

        private List<FirmwarePackage> _Packages;
        public override IList<FirmwarePackage> Packages { get { return _Packages; } }     

        /// <summary>
        /// Создаёт репозиторий по указанному пути
        /// </summary>
        /// <param name="RepositoryRootPath">Пусть к репозиторию</param>
        public DirectoryRepository(String RepositoryRootPath)
            : this(new DirectoryInfo(RepositoryRootPath))
        { }
        /// <summary>
        /// Создаёт репозиторий в указанной директори
        /// </summary>
        /// <param name="RepositoryRoot">Директория с репозиторием</param>
        public DirectoryRepository(DirectoryInfo RepositoryRoot)
        {
            this.RepositoryRoot = RepositoryRoot;
            
            _Packages = 
                RepositoryRoot.Exists ?
                    RepositoryRoot
                        .EnumerateFiles("*." + FirmwarePackage.FirmwarePackageExtension)
                        .Select(f => FirmwarePackage.Open(f))
                        .ToList():
                    new List<FirmwarePackage>();
        }
    }
}
