using System;

namespace FirmwarePacking.SystemsIndexes.Exceptions
{
    /// <Summary>Ошибка при работе с каталогом ячеек</Summary>
    [Serializable]
    public abstract class IndexException : ApplicationException
    {
        private const string ExceptionMessage = "Ошибка при работе с каталогом ячеек";
        protected IndexException() : base(ExceptionMessage) { }
        protected IndexException(string Message) : base(Message) { }
        protected IndexException(Exception inner) : base(ExceptionMessage, inner) { }
    }
}
