using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacking.Exceptions
{
    [Serializable]
    public class FirmwarePackageFormatException : ApplicationException
    {
        public FirmwarePackageFormatException() : base("Ошибка при работе с файлом пакета прошивки") { }
        public FirmwarePackageFormatException(string message) : base(message) { }
        public FirmwarePackageFormatException(string message, Exception inner) : base(message, inner) { }
        protected FirmwarePackageFormatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
