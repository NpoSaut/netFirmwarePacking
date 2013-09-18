using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace FirmwarePacking
{
    public class FirmwareComponent
    {
        public String Name { get; set; }
        public IList<ComponentTarget> Targets { get; set; }
        public IList<FirmwareFile> Files { get; set; }

        private FirmwareComponent()
        {
            Name = Guid.NewGuid().ToString();
        }
        public FirmwareComponent(IList<ComponentTarget> Targets)
            : this()
        {
            this.Targets = Targets;
            Files = new List<FirmwareFile>();
        }
        public FirmwareComponent(XElement XComponent, IEnumerable<FirmwareFile> Files)
            : this()
        {
            Targets = XComponent.Elements("Targets").Select(XTarget => (ComponentTarget)XTarget).ToList();
            this.Files = Files.ToList();
        }

        public override string ToString()
        {
            return string.Format("Component for {0}", string.Join(", ", Targets));
        }
    }
}
