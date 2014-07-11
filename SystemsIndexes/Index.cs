using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FirmwarePacking.SystemsIndexes
{
    public abstract class Index : IIndex
    {
        public abstract ReadOnlyCollection<BlockKind> Blocks { get; }
    }
}
