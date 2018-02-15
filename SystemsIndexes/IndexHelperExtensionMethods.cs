using FirmwarePacking.Annotations;
using FirmwarePacking.SystemsIndexes.Exceptions;

namespace FirmwarePacking.SystemsIndexes
{
    public static class IndexHelperExtensionMethods
    {
        [CanBeNull]
        public static BlockKind TryGetCell([NotNull] this IIndexHelper IndexHelper, int CellId)
        {
            try
            {
                return IndexHelper.GetCell(CellId);
            }
            catch (IndexException)
            {
                return null;
            }
        }
    }
}
