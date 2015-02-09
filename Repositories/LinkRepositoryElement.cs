using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Элемент репозитория, загружающий пакет при запросе</summary>
    public abstract class LinkRepositoryElement : IRepositoryElement
    {
        protected LinkRepositoryElement(PackageInformation Information, ICollection<ComponentTarget> Targets, ReleaseStatus Status = ReleaseStatus.Unknown)
        {
            this.Status = Status;
            this.Information = Information;
            this.Targets = Targets;
        }

        /// <summary>Статус релиза пакета</summary>
        public ReleaseStatus Status { get; private set; }

        /// <summary>Информация о пакете</summary>
        public PackageInformation Information { get; private set; }

        /// <summary>Список целей, для которых имеются компоненты в данном пакете</summary>
        public ICollection<ComponentTarget> Targets { get; private set; }

        /// <summary>Открывает поток для чтения пакета прошивки</summary>
        public abstract Stream GetPackageStream();

        /// <summary>Загружает всё тело пакета</summary>
        public FirmwarePackage GetPackage() { throw new NotImplementedException(); }

        /// <summary>Загружает необходимый компонент из тела пакета</summary>
        /// <param name="Target">Цель, компонент для которой требуется</param>
        public virtual FirmwareComponent GetComponent(ComponentTarget Target) { return GetPackage().Components.Single(c => c.Targets.Contains(Target)); }
    }
}
