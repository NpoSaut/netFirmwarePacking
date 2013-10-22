using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacking.SystemsIndexes
{
    public class ModificationKind
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Id, Name);
        }
    }
}
