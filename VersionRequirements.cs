namespace FirmwarePacking
{
    /// <summary>���������� � ������</summary>
    public class VersionRequirements
    {
        /// <summary>�������������� ����� ��������� ������ <see cref="T:System.Object" />.</summary>
        public VersionRequirements(int Minimum, int Maximum)
        {
            this.Minimum = Minimum;
            this.Maximum = Maximum;
        }

        /// <summary>����������� ����������� ������</summary>
        public int Minimum { get; private set; }

        /// <summary>������������ ����������� ������</summary>
        public int Maximum { get; private set; }

        public bool Intersects(int CompatibleVersion, int ActualVersion) { return CompatibleVersion <= Maximum && ActualVersion >= Minimum; }
    }
}
