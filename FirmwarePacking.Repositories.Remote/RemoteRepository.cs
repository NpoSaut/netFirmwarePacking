using System;
using System.Collections.Generic;
using FirmwarePacking.Repositories.Remote.SautRepoApi;

namespace FirmwarePacking.Repositories.Remote
{
    public class RemoteRepository : IRepository
    {
        private ISfpRepositoryService _repositoryService;

        public RemoteRepository() { _repositoryService = new SfpRepositoryServiceClient(); }

        /// <summary>Список всех пакетов в репозитории</summary>
        public IEnumerable<IRepositoryElement> Packages { get; private set; }

        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Targets">Цели прошивки</param>
        public IEnumerable<IRepositoryElement> GetPackagesForTargets(ICollection<ComponentTarget> Targets) { throw new NotImplementedException(); }
    }
}
