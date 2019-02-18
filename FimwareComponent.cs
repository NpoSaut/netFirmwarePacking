using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FirmwarePacking
{
    public class FirmwareComponent
    {
        private readonly Lazy<IList<FirmwareFile>> _files;

        public FirmwareComponent(IList<ComponentTarget> Targets, IList<FirmwareFile> Files)
        {
            Name = Targets.Aggregate("Component", (name, t) => name += $" {t.CellId}.{t.CellModification}.{t.Module}.{t.Channel}");
            this.Targets = Targets;
            _files = new Lazy<IList<FirmwareFile>>(() => Files);

        }

        public FirmwareComponent(XElement XComponent, IPackageContentProvider ContentProvider)
        {
            Name = XComponent.Attribute("Directory").Value;
            Targets = XComponent.Elements("TargetModule").Select(XTarget => (ComponentTarget)XTarget).ToList();
            BootloaderRequirements = XComponent.Elements("BootloaderRequirement")
                                               .Select(GetBootloaderRequirement)
                                               .ToList();

            _files = new Lazy<IList<FirmwareFile>>(ContentProvider.LoadFirmwareFiles);
        }

        public String Name { get; }
        public IList<ComponentTarget> Targets { get; set; }

        public IList<FirmwareFile> Files => _files.Value;

        public IList<BootloaderRequirement> BootloaderRequirements { get; set; }

        private BootloaderRequirement GetBootloaderRequirement(XElement XRequirement)
        {
            return new BootloaderRequirement((int)XRequirement.Attribute("Id"),
                                             new VersionRequirements((int)XRequirement.Attribute("MinVersion"),
                                                                     (int)XRequirement.Attribute("MaxVersion")));
        }

        public override string ToString() { return $"Component for {string.Join(", ", Targets)}"; }
    }
}
