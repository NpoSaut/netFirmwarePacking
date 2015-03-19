using System.Collections.Generic;

namespace FirmwarePacking.Repositories
{
    /// <summary>��������� ����������� -- ��������� ��������</summary>
    public interface IRepository
    {
        /// <summary>������ ���� ������� � �����������</summary>
        ICollection<IRepositoryElement> Packages { get; }

        /// <summary>������� ����� ��������, ���������� ���������� ��� ���� ��������� �����</summary>
        /// <param name="Targets">���� ��������</param>
        ICollection<IRepositoryElement> GetPackagesForTargets(ICollection<ComponentTarget> Targets);
    }

    /// <summary>�������� ������ ���������� ��� ������ � ������������</summary>
    public static class RepositoryHelper
    {
        /// <summary>������� ����� ��������, ���������� ���������� ��� ���� ��������� �����</summary>
        /// <param name="Repository">����������� ��� ������</param>
        /// <param name="Targets">���� ��������</param>
        public static IEnumerable<IRepositoryElement> GetPackagesForTargets(this IRepository Repository, params ComponentTarget[] Targets)
        {
            return Repository.GetPackagesForTargets(Targets);
        }
    }
}
