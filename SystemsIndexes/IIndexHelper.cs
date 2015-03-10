﻿using System;
using FirmwarePacking.Annotations;

namespace FirmwarePacking.SystemsIndexes
{
    public interface IIndexHelper
    {
        #region Поиск имён

        /// <summary>Находит имя ячейки</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        string GetCellName(int CellId);

        /// <summary>Находит имя программного модуля</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        string GetModuleName(int CellId, int ModuleId);

        /// <summary>Находит имя модификации ячейки</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации ячейки</param>
        string GetModificationName(int CellId, int ModificationId);

        #endregion

        #region Поиск идентификаторов

        /// <summary>Находит идентификатор ячейки</summary>
        /// <param name="CellName">Название ячейки</param>
        int GetCellId(string CellName);

        /// <summary>Находит идентификатор программного модуля</summary>
        /// <param name="CellId">Название ячейки</param>
        /// <param name="ModuleName">Название модуля</param>
        int GetModuleId(int CellId, string ModuleName);

        /// <summary>Находит идентификатор модификации ячейки</summary>
        /// <param name="CellId">Название ячейки</param>
        /// <param name="ModificationName">Название модуля</param>
        int GetModificationId(int CellId, string ModificationName);

        #endregion

        #region Поиск моделей

        /// <summary>Находит модель типа ячейки</summary>
        /// <param name="CellId">Идентификатор типа ячейки</param>
        [NotNull]
        BlockKind GetCell(int CellId);

        /// <summary>Находит модель типа ячейки</summary>
        /// <param name="CellName">Имя ячейки</param>
        [NotNull]
        BlockKind GetCell(string CellName);

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации</param>
        [NotNull]
        ModificationKind GetModification([NotNull] BlockKind Cell, int ModificationId);

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModificationName">Название модификации</param>
        [NotNull]
        ModificationKind GetModification([NotNull] BlockKind Cell, string ModificationName);

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="CellId">Идентификатор типа ячейки</param>
        /// <param name="ModificationId">Идентификатор модификации</param>
        [NotNull]
        ModificationKind GetModification(int CellId, int ModificationId);

        /// <summary>Находит модель модификации ячейки</summary>
        /// <param name="CellName">Название ячейки</param>
        /// <param name="ModificationName">Название модификации</param>
        [NotNull]
        ModificationKind GetModification(String CellName, string ModificationName);

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        [NotNull]
        ModuleKind GetModule([NotNull] BlockKind Cell, int ModuleId);

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="Cell">Модель ячейки</param>
        /// <param name="ModuleName">Идентификатор программного модуля</param>
        [NotNull]
        ModuleKind GetModule([NotNull] BlockKind Cell, string ModuleName);

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="CellId">Идентификатор ячейки</param>
        /// <param name="ModuleId">Идентификатор программного модуля</param>
        [NotNull]
        ModuleKind GetModule(int CellId, int ModuleId);

        /// <summary>Находит модель программного модуля</summary>
        /// <param name="CellName">Имя ячейки</param>
        /// <param name="ModuleName">Имя программного модуля</param>
        [NotNull]
        ModuleKind GetModule(String CellName, string ModuleName);

        #endregion
    }
}
