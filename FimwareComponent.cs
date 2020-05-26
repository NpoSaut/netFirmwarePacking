using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FirmwarePacking.Annotations;

namespace FirmwarePacking
{
    public class FirmwareComponent
    {
        private readonly Lazy<IList<FirmwareFile>> _files;

        public FirmwareComponent([NotNull] IList<ComponentTarget> Targets, [NotNull] IList<ComponentCustomProperty> CustomProperties, [NotNull] IList<FirmwareFile> Files)
        {
            Name = Targets.Aggregate("Component", (name, t) => name += $" {t.CellId}.{t.CellModification}.{t.Module}.{t.Channel}");
            this.Targets = Targets;
            this.CustomProperties = CustomProperties;
            _files = new Lazy<IList<FirmwareFile>>(() => Files);

        }

        public FirmwareComponent([NotNull] XElement XComponent, [NotNull] IPackageContentProvider ContentProvider)
        {
            Name = XComponent.Attribute("Directory").Value;
            Targets = XComponent.Elements("TargetModule")
                                .Select(XTarget => (ComponentTarget)XTarget)
                                .ToList();
            CustomProperties = XComponent.Elements("Property")
                                         .Select(XProperty => (ComponentCustomProperty) XProperty)
                                         .ToList();
            BootloaderRequirements = XComponent.Elements("BootloaderRequirement")
                                               .Select(GetBootloaderRequirement)
                                               .ToList();

            _files = new Lazy<IList<FirmwareFile>>(ContentProvider.LoadFirmwareFiles);
        }

        public String Name { get; }
        public IList<ComponentTarget> Targets { get; }
        public IList<ComponentCustomProperty> CustomProperties { get; }

        public IList<FirmwareFile> Files => _files.Value;

        public IList<BootloaderRequirement> BootloaderRequirements { get; set; }

        private BootloaderRequirement GetBootloaderRequirement(XElement XRequirement)
        {
            return new BootloaderRequirement((int)XRequirement.Attribute("Id"),
                                             new VersionRequirements((int)XRequirement.Attribute("MinVersion"),
                                                                     (int)XRequirement.Attribute("MaxVersion")));
        }

        public override string ToString()
        {
            var customProperties = CustomProperties.Any() ? $"Custom properties: {string.Join(", ", CustomProperties)}" : string.Empty;
            return $"Component for {string.Join(", ", Targets)}{customProperties}";
        }
    }
}
