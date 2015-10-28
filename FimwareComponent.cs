using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FirmwarePacking
{
    public class FirmwareComponent
    {
        private FirmwareComponent() { Name = Guid.NewGuid().ToString(); }

        public FirmwareComponent(IList<ComponentTarget> Targets)
            : this()
        {
            this.Targets = Targets;
            Files = new List<FirmwareFile>();
        }

        public FirmwareComponent(XElement XComponent, IEnumerable<FirmwareFile> Files)
            : this()
        {
            Targets = XComponent.Elements("TargetModule").Select(XTarget => (ComponentTarget)XTarget).ToList();
            this.Files = Files.ToList();
        }

        public String Name { get; set; }
        public IList<ComponentTarget> Targets { get; set; }
        public IList<FirmwareFile> Files { get; set; }
        public ICollection<BootloaderRequirement> SupportedBootloaders { get; private set; }

        public override string ToString() { return string.Format("Component for {0}", string.Join(", ", Targets)); }
    }

    /// <summary>Требования к образу</summary>
    public class BootloaderRequirement
    {
        /// <summary>Требуемый идентификатор загрузчика</summary>
        public int BootloaderId { get; private set; }

        /// <summary>Требования к версии загрузчика</summary>
        public VersionRequirements BootloaderVersion { get; private set; }
    }

    /// <summary>Требования к версии</summary>
    public class VersionRequirements
    {
        /// <summary>Минимальная совместимая версия</summary>
        public int Minimum { get; private set; }

        /// <summary>Максимальная совместимая версия</summary>
        public int Maximum { get; private set; }

        public bool Intersects(int CompatibleVersion, int ActualVersion) { return CompatibleVersion <= Maximum && ActualVersion >= Minimum; }
    }
}
