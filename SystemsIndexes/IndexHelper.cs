using System;
using System.Linq;

namespace FirmwarePacking.SystemsIndexes
{
    public class IndexHelper : IIndexHelper
    {
        private readonly IIndex _index;

        public IndexHelper(IIndex Index)
        {
            if (Index == null)
                throw new ArgumentNullException("Index", "При инициализации IndexHelper был указан пустой индекс");
            _index = Index;
        }

        /// <summary>Находит имя ячейки</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        public string GetCellName(int CellId) { return GetCell(CellId).Name; }

        /// <summary>Находит имя программного модуля</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        public string GetModuleName(int CellId, int ModuleId) { return GetModule(CellId, ModuleId).Name; }

        /// <summary>Находит имя модификации ячейки</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации ячейки</param>
        public string GetModificationName(int CellId, int ModificationId) { return GetModification(CellId, ModificationId).Name; }

        /// <summary>Находит идентификатор ячейки</summary>
        /// <param name="CellName">Название ячейки</param>
        public int GetCellId(string CellName) { return GetCell(CellName).Id; }

        /// <summary>Находит идентификатор программного модуля</summary>
        /// <param name="CellId">Название ячейки</param>
        /// <param name="ModuleName">Название модуля</param>
        public int GetModuleId(int CellId, string ModuleName) { return GetModule(GetCell(CellId), ModuleName).Id; }

        /// <summary>Находит идентификатор модификации ячейки</summary>
        /// <param name="CellId">Название ячейки</param>
        /// <param name="ModificationName">Название модуля</param>
        public int GetModificationId(int CellId, string ModificationName) { return GetModification(GetCell(CellId), ModificationName).Id; }

        /// <summary>Находит модель типа ячейки</summary>
        /// <param name="CellId">Идентификатор типа ячейки</param>
        public BlockKind GetCell(int CellId) { return _index.Blocks.Single(b => b.Id == CellId); }

        /// <summary>Находит модель типа ячейки</summary>
        /// <param name="CellName">Имя ячейки</param>
        public BlockKind GetCell(string CellName) { return _index.Blocks.Single(b => b.Name == CellName); }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации</param>
        public ModificationKind GetModification(BlockKind Cell, int ModificationId) { return Cell.Modifications.Single(m => m.Id == ModificationId); }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModificationName">Название модификации</param>
        public ModificationKind GetModification(BlockKind Cell, string ModificationName) { return Cell.Modifications.Single(m => m.Name == ModificationName); }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="CellId">Идентификатор типа ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации</param>
        public ModificationKind GetModification(int CellId, int ModificationId) { return GetModification(GetCell(CellId), ModificationId); }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="CellName">Название ячейки</param>
        /// <param name="ModificationName">Название модификации</param>
        public ModificationKind GetModification(string CellName, string ModificationName) { return GetModification(GetCell(CellName), ModificationName); }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        public ModuleKind GetModule(BlockKind Cell, int ModuleId) { return Cell.Modules.Single(m => m.Id == ModuleId); }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModuleName">Идентификатор программного модуля</param>
        public ModuleKind GetModule(BlockKind Cell, string ModuleName) { return Cell.Modules.Single(m => m.Name == ModuleName); }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        public ModuleKind GetModule(int CellId, int ModuleId) { return GetModule(GetCell(CellId), ModuleId); }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="CellName">Имя ячейки</param>
        /// <param name="ModuleName">Имя программного модуля</param>
        public ModuleKind GetModule(string CellName, string ModuleName) { return GetModule(GetCell(CellName), ModuleName); }
    }
}
