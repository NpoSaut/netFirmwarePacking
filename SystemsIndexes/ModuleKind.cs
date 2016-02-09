using System;
using FirmwarePacking.Annotations;

namespace FirmwarePacking.SystemsIndexes
{
    public class ModuleKind
    {
        public ModuleKind(int Id, string Name, ICustomPropertiesProvider CustomProperties)
        {
            this.CustomProperties = CustomProperties;
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; private set; }

        [NotNull]
        public String Name { get; private set; }

        [NotNull]
        public ICustomPropertiesProvider CustomProperties { get; private set; }

        public override string ToString() { return string.Format("[{0}] {1}", Id, Name); }
    }
}
