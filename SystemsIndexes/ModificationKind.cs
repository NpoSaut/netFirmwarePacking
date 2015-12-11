using System;
using FirmwarePacking.Annotations;

namespace FirmwarePacking.SystemsIndexes
{
    public class ModificationKind
    {
        public ModificationKind(int Id, string Name, string DeviceName, ICustomPropertiesProvider CustomProperties)
        {
            this.Id = Id;
            this.Name = Name;
            this.DeviceName = DeviceName;
            this.CustomProperties = CustomProperties;
        }

        public int Id { get; private set; }

        [NotNull]
        public String Name { get; private set; }

        [NotNull]
        public String DeviceName { get; private set; }

        [NotNull]
        public ICustomPropertiesProvider CustomProperties { get; private set; }

        public override string ToString() { return string.Format("[{0}] {1}", Id, Name); }
    }
}
