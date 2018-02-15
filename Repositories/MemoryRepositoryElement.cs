using System.Collections.Generic;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Локальный элемент репозитория</summary>
    /// <remarks>
    ///     Класс, оборачивающий уже находящийся в памяти <see cref="FirmwarePackage" />, реализуя интерфейс
    ///     <see cref="IRepositoryElement" />, делегируя вызовы напрямую пакету, переданному в аргументах конструктора
    /// </remarks>
    public class MemoryRepositoryElement : IRepositoryElement
    {
        private readonly FirmwarePackage _package;

        public MemoryRepositoryElement(FirmwarePackage Package, ReleaseStatus Status = ReleaseStatus.Unknown)
        {
            _package    = Package;
            this.Status = Status;
        }

        /// <summary>Статус релиза пакета</summary>
        public ReleaseStatus Status { get; }

        /// <summary>Информация о пакете</summary>
        public PackageInformation Information => _package.Information;

        /// <summary>Список целей, для которых имеются компоненты в данном пакете</summary>
        public ICollection<ComponentTarget> Targets
        {
            get { return _package.Components.SelectMany(c => c.Targets).ToList(); }
        }

        /// <summary>Загружает всё тело пакета</summary>
        public FirmwarePackage GetPackage()
        {
            return _package;
        }

        /// <summary>Загружает необходимый компонент из тела пакета</summary>
        /// <param name="Target">Цель, компонент для которой требуется</param>
        public FirmwareComponent GetComponent(ComponentTarget Target)
        {
            return _package.Components.Single(c => c.Targets.Contains(Target));
        }
    }
}
