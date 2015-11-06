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
            BootloaderRequirement = GetBootloaderRequirement(XComponent.Element("BootloaderRequirement"));
            this.Files = Files.ToList();
        }

        public String Name { get; set; }
        public IList<ComponentTarget> Targets { get; set; }
        public IList<FirmwareFile> Files { get; set; }
        public BootloaderRequirement BootloaderRequirement { get; private set; }

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

    /// <summary>Требования к образу</summary>
    public class BootloaderRequirement
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public BootloaderRequirement(int Id, VersionRequirements Version)
        {
            BootloaderId = Id;
            BootloaderVersion = Version;
        }

        /// <summary>Требуемый идентификатор загрузчика</summary>
        public int BootloaderId { get; private set; }

        /// <summary>Требования к версии загрузчика</summary>
        public VersionRequirements BootloaderVersion { get; private set; }
    }

    /// <summary>Требования к версии</summary>
    public class VersionRequirements
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public VersionRequirements(int Minimum, int Maximum)
        {
            this.Minimum = Minimum;
            this.Maximum = Maximum;
        }

        /// <summary>Минимальная совместимая версия</summary>
        public int Minimum { get; private set; }

        /// <summary>Максимальная совместимая версия</summary>
        public int Maximum { get; private set; }

        public bool Intersects(int CompatibleVersion, int ActualVersion) { return CompatibleVersion <= Maximum && ActualVersion >= Minimum; }
    }
}
