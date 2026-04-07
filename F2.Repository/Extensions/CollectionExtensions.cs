namespace F2.Repository.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    /// <summary>
    /// Merges an existing collection (e.g. an EF-tracked collection) with a collection of DTOs.
    /// For each DTO:
    /// - If the DTO's key (obtained via <paramref name="rightKey"/>) is <c>default</c> or does not exist in the collection,
    ///   a new entity is created and added.
    /// - If the key exists, the existing entity is updated using the <paramref name="update"/> action.
    /// Finally, any entities whose valid keys are not present in the DTO collection are removed.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity (e.g. OrderItem).</typeparam>
    /// <typeparam name="TDto">The type of the DTO (e.g. OrderItemDto).</typeparam>
    /// <typeparam name="TKey">The type of the key (e.g. Guid, int, long).</typeparam>
    /// <param name="entities">The existing collection to update.</param>
    /// <param name="data">The incoming DTO collection.</param>
    /// <param name="leftKey">A function to extract the key from an entity.</param>
    /// <param name="rightKey">A function to extract the key from a DTO.</param>
    /// <param name="update">An action to update an existing entity with data from a DTO.</param>
    /// <param name="create">A function to create a new entity from a DTO.</param>
    /// <example>
    /// Usage:
    /// <code>
    /// preOrder.Items.MergeByKey(
    ///     data: request.Items,
    ///     leftKey: item => item.Id,
    ///     rightKey: dto => dto.Id,
    ///     update: (existingItem, dto) =>
    ///     {
    ///         existingItem.ProductName = dto.ProductName;
    ///         existingItem.ProductCode = dto.ProductCode;
    ///         existingItem.Quantity = dto.Quantity;
    ///         existingItem.Confidence = dto.Confidence;
    ///     },
    ///     create: dto => new OrderItem
    ///     {
    ///         ProductName = dto.ProductName,
    ///         ProductCode = dto.ProductCode,
    ///         Quantity = dto.Quantity,
    ///         Confidence = dto.Confidence,
    ///     }
    /// );
    /// </code>
    /// </example>
    public static void MergeByKey<TEntity, TDto, TKey>(
        this ICollection<TEntity> entities,
        IEnumerable<TDto> data,
        Func<TEntity, TKey> leftKey,
        Func<TDto, TKey> rightKey,
        Action<TEntity, TDto> update,
        Func<TDto, TEntity> create)
        where TKey : struct, IEquatable<TKey>
    {
        ArgumentNullException.ThrowIfNull(entities);
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(leftKey);
        ArgumentNullException.ThrowIfNull(rightKey);
        ArgumentNullException.ThrowIfNull(update);
        ArgumentNullException.ThrowIfNull(create);

        var defaultKey = default(TKey);

        // Build a dictionary of existing entities (excluding those with default key)
        var entityMap = entities
            .Where(e => !leftKey(e).Equals(defaultKey))
            .ToDictionary(e => leftKey(e), e => e);

        // Build a set of keys from the DTOs, excluding default ones,
        // so they don't affect the deletion phase.
        var dtoKeys = new HashSet<TKey>(data.Select(dto => rightKey(dto)).Where(id => !id.Equals(defaultKey)));

        // 1) Upsert operation: for each DTO,
        //    if the key is default or not found in the map, create and add a new entity;
        //    otherwise, update the existing entity.
        foreach (var dto in data)
        {
            var dtoKey = rightKey(dto);
            if (dtoKey.Equals(defaultKey) || !entityMap.TryGetValue(dtoKey, out var existingEntity))
            {
                var newEntity = create(dto);
                entities.Add(newEntity);
            }
            else
            {
                update(existingEntity, dto);
            }
        }

        // 2) Remove from the collection any entities whose valid key is not present in the DTO set.
        var itemsToRemove = entities.Where(e => !leftKey(e).Equals(defaultKey) && !dtoKeys.Contains(leftKey(e))).ToArray();

        foreach (var item in itemsToRemove)
        {
            entities.Remove(item);
        }
    }
}