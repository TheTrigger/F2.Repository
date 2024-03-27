using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Security.Cryptography;

namespace Oibi.Repository.ValueGenerator;

public class SecureGuidValueGenerator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry)
    {
        return CreateCryptographicallySecureGuid();
    }

    private static Guid CreateCryptographicallySecureGuid()
    {
        // Determina l'indice del byte di versione in base all'endianess del sistema
        int versionByteIndex = BitConverter.IsLittleEndian ? 7 : 6;

        // Ottieni byte randomici crittograficamente sicuri
        Span<byte> bytes = stackalloc byte[16];
        RandomNumberGenerator.Fill(bytes);

        // Imposta i bit della versione (Version 4)
        bytes[versionByteIndex] = (byte)((bytes[versionByteIndex] & 0x0F) | 0x40);

        // Imposta i bit della variante (RFC 4122)
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

        return new Guid(bytes);
    }
}