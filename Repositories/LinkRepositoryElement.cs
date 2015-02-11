using System;
using System.Collections.Generic;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Элемент репозитория, загружающий пакет при запросе</summary>
    public abstract class LinkRepositoryElement : IRepositoryElement
    {
        private readonly Lazy<FirmwarePackage> _package;

        protected LinkRepositoryElement(PackageInformation Information, ICollection<ComponentTarget> Targets, ReleaseStatus Status = ReleaseStatus.Unknown)
        {
            this.Status = Status;
            this.Information = Information;
            this.Targets = Targets;
            _package = new Lazy<FirmwarePackage>(LoadPackage);
        }

        /// <summary>Статус релиза пакета</summary>
        public ReleaseStatus Status { get; private set; }

        /// <summary>Информация о пакете</summary>
        public PackageInformation Information { get; private set; }

        /// <summary>Список целей, для которых имеются компоненты в данном пакете</summary>
        public ICollection<ComponentTarget> Targets { get; private set; }

        /// <summary>Загружает всё тело пакета</summary>
        public FirmwarePackage GetPackage() { return _package.Value; }

        /// <summary>Загружает необходимый компонент из тела пакета</summary>
        /// <param name="Target">Цель, компонент для которой требуется</param>
        public virtual FirmwareComponent GetComponent(ComponentTarget Target) { return GetPackage().Components.Single(c => c.Targets.Contains(Target)); }

        /// <summary>Производит загрузку пакета прошивки по ссылке</summary>
        protected abstract FirmwarePackage LoadPackage();
    }
}
