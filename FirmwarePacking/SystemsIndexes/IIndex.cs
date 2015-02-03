using System.Collections.ObjectModel;

namespace FirmwarePacking.SystemsIndexes
{
    public interface IIndex
    {
        ReadOnlyCollection<BlockKind> Blocks { get; }
    }
}
