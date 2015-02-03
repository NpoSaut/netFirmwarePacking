using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacking.Exceptions
{
    [Serializable]
    public class FirmwarePackageUncompatibleFormatException : FirmwarePackageFormatException
    {
        public int PackageVersion { get; set; }

        public FirmwarePackageUncompatibleFormatException() : base("Версия формата пакета прошивки не поддерживается") { }
        public FirmwarePackageUncompatibleFormatException(string message) : base(message) { }
        public FirmwarePackageUncompatibleFormatException(string message, Exception inner) : base(message, inner) { }
        protected FirmwarePackageUncompatibleFormatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
