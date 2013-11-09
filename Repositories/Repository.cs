using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacking.Repositories
{
    /// <summary>
    /// Представляет абстрактную модель репозитория с прошивками
    /// </summary>
    public abstract class Repository
    {
        public abstract IList<FirmwarePackage> Packages { get; }

        /// <summary>
        /// Находит пакет прошивки, содержащий компоненты для всех указанных целей
        /// </summary>
        /// <param name="Targets">Цели прошивки</param>
        public IEnumerable<FirmwarePackage> GetPackagesForTargets(params ComponentTarget[] Targets) { return GetPackagesForTargets((IList<ComponentTarget>)Targets); }
        /// <summary>
        /// Находит пакет прошивки, содержащий компоненты для всех указанных целей
        /// </summary>
        /// <param name="Targets">Цели прошивки</param>
        public virtual IEnumerable<FirmwarePackage> GetPackagesForTargets(IList<ComponentTarget> Targets)
        {
            return Packages.Where(p => Targets.All(t => p.Components.SelectMany(c => c.Targets).Contains(t)));
        }
    }
}
