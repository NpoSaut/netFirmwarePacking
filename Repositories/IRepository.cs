using System.Collections.Generic;

namespace FirmwarePacking.Repositories
{
    /// <summary>Интерфейс репозитория -- хранилища прошивок</summary>
    public interface IRepository
    {
        /// <summary>Список всех пакетов в репозитории</summary>
        IList<FirmwarePackage> Packages { get; }

        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Targets">Цели прошивки</param>
        IEnumerable<FirmwarePackage> GetPackagesForTargets(IList<ComponentTarget> Targets);
    }

    /// <summary>Содержит методы расширения для работы с репозиторием</summary>
    public static class RepositoryHelper
    {
        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Repository">Репозиторий для поиска</param>
        /// <param name="Targets">Цели прошивки</param>
        public static IEnumerable<FirmwarePackage> GetPackagesForTargets(this IRepository Repository, params ComponentTarget[] Targets)
        {
            return Repository.GetPackagesForTargets(Targets);
        }
    }
}
