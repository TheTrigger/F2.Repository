using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Oibi.Repository.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Set <see cref="ConfigureAsUtcDateTime"/> to <see cref="DateTimeOffset"/> and nullable <see cref="DateTimeOffset?"/>
    /// </summary>
    public static ModelBuilder UseUtcDateTimeOffset(this ModelBuilder modelBuilder, params Type[] excludedEntityTypes)
    {
        var excludedSet = new HashSet<Type>(excludedEntityTypes);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType is null) continue;
            if (entityType.HasSharedClrType) continue;
            if (excludedSet.Contains(entityType.ClrType)) continue;

            modelBuilder.Entity(entityType.ClrType, builder =>
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTimeOffset))
                    {
                        var propertyBuilder = builder.Property<DateTimeOffset>(property.Name);
                        propertyBuilder.ConfigureAsUtcDateTime();
                    }
                    else if (property.ClrType == typeof(DateTimeOffset?) && property.IsNullable)
                    {
                        var propertyBuilder = builder.Property<DateTimeOffset?>(property.Name);
                        propertyBuilder.ConfigureAsUtcDateTime();
                    }
                }
            });
        }

        return modelBuilder;
    }
}