namespace FirmwarePacking
{
    /// <summary>���������� � ������</summary>
    public class BootloaderRequirement
    {
        /// <summary>�������������� ����� ��������� ������ <see cref="T:System.Object" />.</summary>
        public BootloaderRequirement(int Id, VersionRequirements Version)
        {
            BootloaderId = Id;
            BootloaderVersion = Version;
        }

        /// <summary>��������� ������������� ����������</summary>
        public int BootloaderId { get; private set; }

        /// <summary>���������� � ������ ����������</summary>
        public VersionRequirements BootloaderVersion { get; private set; }
    }
}