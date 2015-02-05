using System.Collections.Generic;

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
        public FileLinkRepositoryElement(PackageInformation Information, ICollection<ComponentTarget> Targets, string FileName) : base(Information, Targets)
        {
            _fileName = FileName;
        }

        /// <summary>Производит загрузку пакета прошивки по ссылке</summary>
        protected override FirmwarePackage LoadPackage()
        {
            return FirmwarePackage.Open(_fileName);
        }
    }
}
