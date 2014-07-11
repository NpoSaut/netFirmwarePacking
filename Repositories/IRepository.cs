using System.Collections.Generic;

namespace FirmwarePacking.Repositories
{
    public interface IRepository
    {
        IList<FirmwarePackage> Packages { get; }
    }
}
