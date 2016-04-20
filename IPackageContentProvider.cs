using System.Collections.Generic;

namespace FirmwarePacking
{
    public interface IPackageContentProvider
    {
        IList<FirmwareFile> LoadFirmwareFiles();
    }
}
