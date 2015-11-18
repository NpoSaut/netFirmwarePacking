namespace FirmwarePacking
{
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
