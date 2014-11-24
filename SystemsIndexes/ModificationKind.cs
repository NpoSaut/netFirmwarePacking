using System;

namespace FirmwarePacking.SystemsIndexes
{
    public class ModificationKind
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DeviceKind  Device { get; set; }

        public override string ToString() { return string.Format("[{0}] {1}", Id, Name); }
    }
}
