using System.Collections.Generic;

namespace FirmwarePacking.Repositories
{
    /// <summary>Интерфейс репозитория -- хранилища прошивок</summary>
    public interface IRepository
    {
        /// <summary>Список всех пакетов в репозитории</summary>
        ICollection<IRepositoryElement> Packages { get; }

        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Targets">Цели прошивки</param>
        ICollection<IRepositoryElement> GetPackagesForTargets(ICollection<ComponentTarget> Targets);
    }

    /// <summary>Содержит методы расширения для работы с репозиторием</summary>
    public static class RepositoryHelper
    {
        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Repository">Репозиторий для поиска</param>
        /// <param name="Targets">Цели прошивки</param>
        public static IEnumerable<IRepositoryElement> GetPackagesForTargets(this IRepository Repository, params ComponentTarget[] Targets)
        {
            return Repository.GetPackagesForTargets(Targets);
        }
    }
}
