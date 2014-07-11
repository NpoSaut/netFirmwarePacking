using System;
using System.Linq;

namespace FirmwarePacking.SystemsIndexes
{
    public interface IIndexHelper
    {
        String GetCellName(int CellId);
        String GetModuleName(int CellId, int ModuleId);
        String GetModificationName(int CellId, int ModificationId);
    }

    public class IndexHelper : IIndexHelper
    {
        private readonly IIndex _index;
        public IndexHelper(IIndex Index) { _index = Index; }

        public string GetCellName(int CellId)
        {
            return _index.Blocks.Where(b => b.Id == CellId).Select(b => b.Name).DefaultIfEmpty("Неизвестная ячейка").First();
        }

        public string GetModuleName(int CellId, int ModuleId)
        {
            return
                _index.Blocks.Where(b => b.Id == CellId)
                      .SelectMany(b => b.Modules.Where(m => m.Id == ModuleId))
                      .Select(b => b.Name)
                      .DefaultIfEmpty("Неизвестный модуль")
                      .First();
        }

        public string GetModificationName(int CellId, int ModificationId)
        {
            return
                _index.Blocks.Where(b => b.Id == CellId)
                      .SelectMany(b => b.Modifications.Where(m => m.Id == ModificationId))
                      .Select(b => b.Name)
                      .DefaultIfEmpty("Неизвестная модификация")
                      .First();
        }
    }
}
