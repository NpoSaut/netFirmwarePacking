using System.Collections.ObjectModel;

namespace FirmwarePacking.SystemsIndexes
{
    public abstract class Index : IIndex
    {
        public abstract ReadOnlyCollection<BlockKind> Blocks { get; }
    }
}
