using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacking.SystemsIndexes
{
    public class BlockKind
    {
        public int Id { get; set; }
        public String Name { get; set; }

        private List<ModuleKind> _Modules;
        public List<ModuleKind> Modules
        {
            get { return _Modules; }
            set
            {
                if (value == null || value.Count == 0)
                    _Modules = new List<ModuleKind>() { new ModuleKind() { Id = 0, Name = "Основной модуль" } };
                else
                    _Modules = value;
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Id, Name);
        }


        public int ChannelsCount { get; set; }
    }
}
