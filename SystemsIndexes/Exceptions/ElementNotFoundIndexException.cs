using System;

namespace FirmwarePacking.SystemsIndexes.Exceptions
{
    /// <Summary>Элемент не найден в каталоге</Summary>
    [Serializable]
    public abstract class ElementNotFoundIndexException : IndexException
    {
        protected ElementNotFoundIndexException(string Message) : base(Message) { }
    }

    /// <Summary>Ячейка не найдена в каталоге</Summary>
    [Serializable]
    public class CellNotFoundIndexException : ElementNotFoundIndexException
    {
        public CellNotFoundIndexException(object CellId)
            : base(string.Format("Ячейка не найдена в каталоге Cell={0}", CellId)) { }
    }

    /// <Summary>Модификация не найдена в каталоге</Summary>
    [Serializable]
    public class ModificationNotFoundIndexException : ElementNotFoundIndexException
    {
        public ModificationNotFoundIndexException(object CellId, object ModificationId)
            : base(string.Format("Модификация не найдена в каталоге CellId={0}, ModuleId={1}", CellId, ModificationId)) { }
    }

    /// <Summary>Программный модуль не найден в каталоге</Summary>
    [Serializable]
    public class ModuleNotFoundIndexException : ElementNotFoundIndexException
    {
        public ModuleNotFoundIndexException(object CellId, object ModuleId)
            : base(string.Format("Программный модуль не найден в каталоге CellId={0}, ModuleId={1}", CellId, ModuleId)) { }
    }
}
