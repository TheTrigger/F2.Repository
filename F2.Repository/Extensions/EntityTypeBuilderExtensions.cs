﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using F2.Repository.Configurations.Converters;
using F2.Repository.Interfaces;
using F2.Repository.ValueGenerator;
using System;

namespace F2.Repository.Extensions;

public static class EntityTypeBuilderExtensions
{
    private static readonly DateTimeOffsetToUtcConverter _dateTimeOffsetToUtcConverter = new();

    public static EntityTypeBuilder<TEntity> UseTimestampedProperty<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : class, ITimestampedEntity
    {
        entity.Property(d => d.CreatedAt).ConfigureAsUtcDateTime().ValueGeneratedOnAdd();
        entity.Property(d => d.UpdatedAt).ConfigureAsUtcDateTime().ValueGeneratedOnAddOrUpdate();
        return entity;
    }

    public static EntityTypeBuilder<TEntity> UseAutoGeneratedId<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity<Guid>
    {
        entity.Property(d => d.Id).HasValueGenerator<SecureGuidValueGenerator>().ValueGeneratedOnAdd();

        return entity;
    }

    public static PropertyBuilder<DateTimeOffset> ConfigureAsUtcDateTime(this PropertyBuilder<DateTimeOffset> builder)
    {
        return builder.HasConversion(_dateTimeOffsetToUtcConverter)
            .HasValueGenerator<DateTimeOffsetValueGenerator>()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }

    public static PropertyBuilder<DateTimeOffset?> ConfigureAsUtcDateTime(this PropertyBuilder<DateTimeOffset?> builder)
    {
        return builder.HasConversion(_dateTimeOffsetToUtcConverter)
            .HasValueGenerator<DateTimeOffsetValueGenerator>();
    }
}