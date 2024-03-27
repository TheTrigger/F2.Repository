using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Oibi.Repository.Demo.Models.Configurations.Converters;

public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter() : base(
            v => v.ToUniversalTime(),  // Converti da DateTime a DateTime UTC per il database
            v => v // Converti da DateTime UTC del database a DateTime con DateTimeKind.Utc
        )
    {
    }
}