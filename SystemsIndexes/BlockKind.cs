using System;
using System.Collections.Generic;
using System.Linq;

namespace FirmwarePacking.SystemsIndexes
{
    public class BlockKind
    {
        public BlockKind(int Id, string Name, int ChannelsCount, List<ModuleKind> Modules, List<ModificationKind> Modifications)
        {
            this.Id = Id;
            this.Name = Name;
            this.Modifications = Modifications;
            this.ChannelsCount = ChannelsCount;
            this.Modules =
                Modules.Any()
                    ? Modules
                    : DefaultModules;
        }

        public int Id { get; private set; }
        public String Name { get; private set; }
        public List<ModuleKind> Modules { get; private set; }
        public List<ModificationKind> Modifications { get; private set; }

        private static List<ModuleKind> DefaultModules
        {
            get
            {
                return new List<ModuleKind> { new ModuleKind(1, "Основной модуль", new DictionaryCustomPropertiesProvider(new Dictionary<string, string>())) };
            }
        }

        public int ChannelsCount { get; set; }
        public override string ToString() { return string.Format("[{0}] {1}", Id, Name); }
    }
}
