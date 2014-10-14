using System.Collections.Generic;

namespace FirmwarePacking.Repositories
{
    /// <summary>��������� ����������� -- ��������� ��������</summary>
    public interface IRepository
    {
        /// <summary>������ ���� ������� � �����������</summary>
        IList<FirmwarePackage> Packages { get; }

        /// <summary>������� ����� ��������, ���������� ���������� ��� ���� ��������� �����</summary>
        /// <param name="Targets">���� ��������</param>
        IEnumerable<FirmwarePackage> GetPackagesForTargets(IList<ComponentTarget> Targets);
    }

    /// <summary>�������� ������ ���������� ��� ������ � ������������</summary>
    public static class RepositoryHelper
    {
        /// <summary>������� ����� ��������, ���������� ���������� ��� ���� ��������� �����</summary>
        /// <param name="Repository">����������� ��� ������</param>
        /// <param name="Targets">���� ��������</param>
        public static IEnumerable<FirmwarePackage> GetPackagesForTargets(this IRepository Repository, params ComponentTarget[] Targets)
        {
            return Repository.GetPackagesForTargets(Targets);
        }
    }
}
