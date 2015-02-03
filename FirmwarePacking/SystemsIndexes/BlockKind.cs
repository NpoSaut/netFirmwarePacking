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
                    _Modules = DefaultModules;
                else
                    _Modules = value;
            }
        }

        private List<ModificationKind> _Modifications;
        public List<ModificationKind> Modifications
        {
            get { return _Modifications; }
            set
            {
                if (value == null || value.Count == 0)
                    _Modifications = DefaultModifications;
                else
                    _Modifications = value;
            }
        }

        public BlockKind()
        {
            Modules = DefaultModules;
            Modifications = DefaultModifications;
        }


        internal static List<ModuleKind> DefaultModules
        {
            get { return new List<ModuleKind>() { new ModuleKind() { Id = 1, Name = "Основной модуль" } }; }
        }
        internal static List<ModificationKind> DefaultModifications
        {
            get { return new List<ModificationKind>() { new ModificationKind() { Id = 1, Name = "Базовая" } }; }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Id, Name);
        }


        public int ChannelsCount { get; set; }
    }
}
