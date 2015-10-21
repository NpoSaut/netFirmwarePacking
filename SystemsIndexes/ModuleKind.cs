using System;

namespace FirmwarePacking.SystemsIndexes
{
    public class ModuleKind
    {
        public ModuleKind(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; private set; }
        public String Name { get; private set; }

        public override string ToString() { return string.Format("[{0}] {1}", Id, Name); }
    }
}
