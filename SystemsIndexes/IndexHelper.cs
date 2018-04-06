using System;
using System.Linq;
using FirmwarePacking.SystemsIndexes.Exceptions;

namespace FirmwarePacking.SystemsIndexes
{
    public class IndexHelper : IIndexHelper
    {
        private static readonly Lazy<IIndexHelper> _defaultIndexHelper = new Lazy<IIndexHelper>(() => new IndexHelper(XmlIndex.Default));
        public static IIndexHelper Default => _defaultIndexHelper.Value;

        private readonly IIndex _index;

        public IndexHelper(IIndex Index)
        {
            if (Index == null)
                throw new ArgumentNullException("Index", "При инициализации IndexHelper был указан пустой индекс");
            _index = Index;
        }

        /// <summary>Находит имя ячейки</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        public string GetCellName(int CellId)
        {
            try
            {
                return GetCell(CellId).Name;
            }
            catch (IndexException)
            {
                return "Неизвестная ячейка";
            }
        }

        /// <summary>Находит имя программного модуля</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        public string GetModuleName(int CellId, int ModuleId)
        {
            try
            {
                return GetModule(CellId, ModuleId).Name;
            }
            catch (IndexException)
            {
                return "Неизвестный модуль";
            }
        }

        /// <summary>Находит имя модификации ячейки</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации ячейки</param>
        public string GetModificationName(int CellId, int ModificationId)
        {
            try
            {
                return GetModification(CellId, ModificationId).Name;
            }
            catch (IndexException)
            {
                return "Неизвестная модификация";
            }
        }

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
        /// <exception cref="CellNotFoundIndexException">Ячейка не найдена в каталоге</exception>
        public BlockKind GetCell(int CellId)
        {
            BlockKind cell = _index.Blocks.SingleOrDefault(b => b.Id == CellId);
            if (cell == null)
                throw new CellNotFoundIndexException(CellId);
            return cell;
        }

        /// <summary>Находит модель типа ячейки</summary>
        /// <param name="CellName">Имя ячейки</param>
        /// <exception cref="CellNotFoundIndexException">Ячейка не найдена в каталоге</exception>
        public BlockKind GetCell(string CellName)
        {
            BlockKind cell = _index.Blocks.SingleOrDefault(b => b.Name == CellName);
            if (cell == null)
                throw new CellNotFoundIndexException(CellName);
            return cell;
        }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации</param>
        /// <exception cref="ModificationNotFoundIndexException">Модификация ячейки не найдена в каталоге</exception>
        public ModificationKind GetModification(BlockKind Cell, int ModificationId)
        {
            ModificationKind modification = Cell.Modifications.SingleOrDefault(m => m.Id == ModificationId);
            if (modification == null)
                throw new ModificationNotFoundIndexException(Cell.Name, ModificationId);
            return modification;
        }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModificationName">Название модификации</param>
        /// <exception cref="ModificationNotFoundIndexException">Модификация ячейки не найдена в каталоге</exception>
        public ModificationKind GetModification(BlockKind Cell, string ModificationName)
        {
            ModificationKind modification = Cell.Modifications.SingleOrDefault(m => m.Name == ModificationName);
            if (modification == null)
                throw new ModificationNotFoundIndexException(Cell.Name, ModificationName);
            return modification;
        }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="CellId">Идентификатор типа ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации</param>
        /// <exception cref="CellNotFoundIndexException">Ячейка не найдена в каталоге</exception>
        /// <exception cref="ModificationNotFoundIndexException">Модификация ячейки не найдена в каталоге</exception>
        public ModificationKind GetModification(int CellId, int ModificationId) { return GetModification(GetCell(CellId), ModificationId); }

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="CellName">Название ячейки</param>
        /// <param name="ModificationName">Название модификации</param>
        /// <exception cref="CellNotFoundIndexException">Ячейка не найдена в каталоге</exception>
        /// <exception cref="ModificationNotFoundIndexException">Модификация ячейки не найдена в каталоге</exception>
        public ModificationKind GetModification(string CellName, string ModificationName) { return GetModification(GetCell(CellName), ModificationName); }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        /// <exception cref="ModuleNotFoundIndexException">Программный модуль не найден в каталоге</exception>
        public ModuleKind GetModule(BlockKind Cell, int ModuleId)
        {
            ModuleKind module = Cell.Modules.SingleOrDefault(m => m.Id == ModuleId);
            if (module == null)
                throw new ModuleNotFoundIndexException(Cell.Name, ModuleId);
            return module;
        }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModuleName">Идентификатор программного модуля</param>
        /// <exception cref="ModuleNotFoundIndexException">Программный модуль не найден в каталоге</exception>
        public ModuleKind GetModule(BlockKind Cell, string ModuleName)
        {
            ModuleKind module = Cell.Modules.SingleOrDefault(m => m.Name == ModuleName);
            if (module == null)
                throw new ModuleNotFoundIndexException(Cell.Name, ModuleName);
            return module;
        }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        /// <exception cref="CellNotFoundIndexException">Ячейка не найдена в каталоге</exception>
        /// <exception cref="ModuleNotFoundIndexException">Программный модуль не найден в каталоге</exception>
        public ModuleKind GetModule(int CellId, int ModuleId) { return GetModule(GetCell(CellId), ModuleId); }

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="CellName">Имя ячейки</param>
        /// <param name="ModuleName">Имя программного модуля</param>
        /// <exception cref="CellNotFoundIndexException">Ячейка не найдена в каталоге</exception>
        /// <exception cref="ModuleNotFoundIndexException">Программный модуль не найден в каталоге</exception>
        public ModuleKind GetModule(string CellName, string ModuleName) { return GetModule(GetCell(CellName), ModuleName); }
    }
}
