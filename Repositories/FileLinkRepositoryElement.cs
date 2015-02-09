using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Элемент репозитория, загружающий пакет из указанного файла при запросе</summary>
    public class FileLinkRepositoryElement : LinkRepositoryElement
    {
        private readonly string _fileName;

        /// <summary>Создаёт элемент репозитория, ссылающийся на файл</summary>
        /// <param name="Information">Информация о пакете</param>
        /// <param name="Targets">Цели пакета</param>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="Status">Статус релиза</param>
        public FileLinkRepositoryElement(PackageInformation Information, ICollection<ComponentTarget> Targets, string FileName, ReleaseStatus Status) : base(Information, Targets, Status)
        {
            _fileName = FileName;
        }

        public static FileLinkRepositoryElement Load(FileInfo FileInfo, ReleaseStatus Status = ReleaseStatus.Unknown)
        {
            FirmwarePackage package = FirmwarePackage.Open(FileInfo);
            return new FileLinkRepositoryElement(
                package.Information,
                package.Components.SelectMany(c => c.Targets).ToList(),
                FileInfo.FullName,
                Status);
        }

        /// <summary>Открывает поток для чтения пакета прошивки</summary>
        public override Stream GetPackageStream()
        {
            return new FileStream(_fileName, FileMode.Open);
        }
    }
}
