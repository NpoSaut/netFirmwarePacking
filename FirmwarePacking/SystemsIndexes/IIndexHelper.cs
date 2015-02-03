using System;
using System.Linq;

namespace FirmwarePacking.SystemsIndexes
{
    public interface IIndexHelper
    {
        string GetCellName(int CellId);
        string GetModuleName(int CellId, int ModuleId);
        string GetModificationName(int CellId, int ModificationId);

        int GetCellId(string CellName);
        int GetModuleId(int CellId, string ModuleName);
        int GetModificationId(int CellId, string ModificationName);
    }

    public class IndexHelper : IIndexHelper
    {
        private readonly IIndex _index;
        public IndexHelper(IIndex Index) { _index = Index; }

        public string GetCellName(int CellId)
        {
            return _index.Blocks.Where(b => b.Id == CellId).Select(b => b.Name).DefaultIfEmpty(String.Format("Неизвестная ячейка (#{0})", CellId)).First();
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

        public int GetCellId(string CellName) { return _index.Blocks.Single(b => b.Name == CellName).Id; }

        public int GetModuleId(int CellId, string ModuleName)
        {
            var block = _index.Blocks
                              .Single(b => b.Id == CellId);

            var module = block.Modules
                         .Single(m => m.Name == ModuleName);
            return module.Id;
        }

        public int GetModificationId(int CellId, string ModificationName)
        {
            return _index.Blocks
                         .Single(b => b.Id == CellId).Modifications
                         .Single(m => m.Name == ModificationName).Id;
        }
    }
}
