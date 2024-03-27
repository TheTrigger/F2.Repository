using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Oibi.Repository.ValueGenerator;

public class DateTimeOffsetValueGenerator : ValueGenerator<DateTimeOffset>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }
}