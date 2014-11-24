using System;
using System.Collections.Generic;

namespace FirmwarePacking.SystemsIndexes
{
    public class DeviceKind
    {
        internal DeviceKind(string Name, Dictionary<string, string> Properties)
        {
            this.Name = Name;
            this.Properties = Properties;
        }

        public String Name { get; private set; }
        public Dictionary<String, String> Properties { get; private set; }
    }
}
