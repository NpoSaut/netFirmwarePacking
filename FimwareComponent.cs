using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FirmwarePacking
{
    public class FirmwareComponent
    {
        private readonly Lazy<IList<FirmwareFile>> _files;

        private FirmwareComponent() { Name = Guid.NewGuid().ToString(); }

        public FirmwareComponent(IList<ComponentTarget> Targets, IEnumerable<FirmwareFile> Files)
            : this()
        {
            this.Targets = Targets;
            _files = new Lazy<IList<FirmwareFile>>(Files.ToList);
        }

        public FirmwareComponent(XElement XComponent, IPackageContentProvider ContentProvider)
            : this()
        {
            Targets = XComponent.Elements("TargetModule").Select(XTarget => (ComponentTarget)XTarget).ToList();
            BootloaderRequirement = GetBootloaderRequirement(XComponent.Element("BootloaderRequirement"));

            _files = new Lazy<IList<FirmwareFile>>(ContentProvider.LoadFirmwareFiles);
        }

        public String Name { get; set; }
        public IList<ComponentTarget> Targets { get; set; }

        public IList<FirmwareFile> Files
        {
            get { return _files.Value; }
        }

        public BootloaderRequirement BootloaderRequirement { get; set; }

        private BootloaderRequirement GetBootloaderRequirement(XElement XRequirement)
        {
            if (XRequirement == null)
                return null;
            return new BootloaderRequirement((int)XRequirement.Attribute("Id"),
                                             new VersionRequirements((int)XRequirement.Attribute("MinVersion"),
                                                                     (int)XRequirement.Attribute("MaxVersion")));
        }

        public override string ToString() { return string.Format("Component for {0}", string.Join(", ", Targets)); }
    }
}
