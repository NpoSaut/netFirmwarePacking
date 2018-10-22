using System.Security.Cryptography;

namespace FirmwarePacking
{
    public static class IdentifierProvider
    {
        private static SHA256Managed _hasher = new SHA256Managed();

        public static string GetIdentifier()
        {
            return "fuck you bitch";
        }
    }
}
