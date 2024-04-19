using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Oibi.Repository.Configurations.Converters;

/// <summary>
/// <see href="https://stackoverflow.com/questions/29127128/how-to-store-datetimeoffset-in-postresql/77460308#77460308"/>
/// </summary>
public class DateTimeOffsetToUtcConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetToUtcConverter() : base(
            convertToProviderExpression: d => d.ToUniversalTime(), // Quando scrivi nel DB, converti in UTC - postgres lo fa già
            convertFromProviderExpression: d => d)
    { }
}
