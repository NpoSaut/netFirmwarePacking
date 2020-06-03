using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FirmwarePacking
{
    public static class IdentifierProvider
    {
        private static readonly SHA256Managed _hasher = new SHA256Managed();

        public static string GetIdentifier(FirmwarePackage Package)
        {
            var hashData =
                Package.Components
                       .Select(c => string.Join(
                                   "-",
                                   c.Targets.Select(t => $"{t.CellId}.{t.CellModification}.{t.Module}.{t.Channel}")))
                       .SelectMany(c => Encoding.Unicode.GetBytes(c))
                       .ToList();

            hashData.AddRange(Encoding.Unicode.GetBytes(Package.Information.FirmwareVersionLabel ?? string.Empty));
            hashData.AddRange(BitConverter.GetBytes(Package.Information.FirmwareVersion.Major));
            hashData.AddRange(BitConverter.GetBytes(Package.Information.FirmwareVersion.Minor));
            hashData.AddRange(BitConverter.GetBytes(Package.Information.ReleaseDate.Ticks));

            return Convert.ToBase64String(_hasher.ComputeHash(hashData.ToArray()));
        }
    }
}