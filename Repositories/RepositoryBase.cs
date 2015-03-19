using System.Collections.Generic;
using System.Linq;

namespace FirmwarePacking.Repositories
{
    /// <summary>Представляет абстрактную модель репозитория с прошивками</summary>
    public abstract class RepositoryBase : IRepository
    {
        /// <summary>Список всех пакетов в репозитории</summary>
        public abstract ICollection<IRepositoryElement> Packages { get; }

        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Targets">Цели прошивки</param>
        public virtual ICollection<IRepositoryElement> GetPackagesForTargets(ICollection<ComponentTarget> Targets)
        {
            return Packages.Where(p => Targets.All(t => p.Targets.Contains(t))).ToList();
        }
    }
}
