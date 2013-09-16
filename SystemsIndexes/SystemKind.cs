using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacking.SystemsIndexes
{
    public class SystemKind
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<BlockKind> Blocks { get; set; }

        public SystemKind()
        { }
        public SystemKind(int Id, String Name)
            : this()
        {
            this.Id = Id;
            this.Name = Name;
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Id, Name);
        }
    }
}
