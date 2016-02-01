namespace FirmwarePacking
{
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

        public override string ToString() { return string.Format("ID загрузчика: {0}, версия: {1}", BootloaderId, BootloaderVersion); }
    }
}
