using System.Collections.Generic;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Представляет абстрактную модель репозитория с прошивками</summary>
    public abstract class Repository : IRepository
    {
        /// <summary>Список всех пакетов в репозитории</summary>
        public abstract IList<FirmwarePackage> Packages { get; }

        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Targets">Цели прошивки</param>
        public virtual IEnumerable<FirmwarePackage> GetPackagesForTargets(IList<ComponentTarget> Targets)
        {
            return Packages.Where(p => Targets.All(t => p.Components.SelectMany(c => c.Targets).Contains(t)));
        }
    }
}
