namespace F2.Repository.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    /// <summary>
    /// Merges an existing collection (e.g. an EF-tracked collection) with a collection of DTOs.
    /// For each DTO:
    /// - If the DTO's ID (obtained via <paramref name="rightKey"/>) is Guid.Empty or does not exist in the collection,
    ///   a new entity is created and added.
    /// - If the ID exists, the existing entity is updated using the <paramref name="update"/> action.
    /// Finally, any entities whose valid IDs are not present in the DTO collection are removed.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity (e.g. OrderItem).</typeparam>
    /// <typeparam name="TDto">The type of the DTO (e.g. OrderItemDto).</typeparam>
    /// <param name="entities">The existing collection to update.</param>
    /// <param name="data">The incoming DTO collection.</param>
    /// <param name="leftKey">A function to extract the ID from an entity.</param>
    /// <param name="rightKey">A function to extract the ID from a DTO.</param>
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
    public static void MergeByKey<TEntity, TDto>(
        this ICollection<TEntity> entities,
        IEnumerable<TDto> data,
        Func<TEntity, Guid> leftKey,
        Func<TDto, Guid> rightKey,
        Action<TEntity, TDto> update,
        Func<TDto, TEntity> create)
    {
        ArgumentNullException.ThrowIfNull(entities);
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(leftKey);
        ArgumentNullException.ThrowIfNull(rightKey);
        ArgumentNullException.ThrowIfNull(update);
        ArgumentNullException.ThrowIfNull(create);

        // Build a dictionary of existing entities (excluding those with Guid.Empty)
        var entityMap = entities
            .Where(e => leftKey(e) != Guid.Empty)
            .ToDictionary(e => leftKey(e), e => e);

        // Build a set of IDs from the DTOs, excluding empty ones,
        // so they don't affect the deletion phase.
        var dtoIds = new HashSet<Guid>(data.Select(dto => rightKey(dto)).Where(id => id != Guid.Empty));

        // 1) Upsert operation: for each DTO,
        //    if the ID is Guid.Empty or not found in the map, create and add a new entity;
        //    otherwise, update the existing entity.
        foreach (var dto in data)
        {
            var dtoId = rightKey(dto);
            if (dtoId == Guid.Empty || !entityMap.TryGetValue(dtoId, out var existingEntity))
            {
                // Create a new entity and add it to the collection
                var newEntity = create(dto);
                entities.Add(newEntity);
            }
            else
            {
                // Update the existing entity
                update(existingEntity, dto);
            }
        }

        // 2) Remove from the collection any entities whose valid ID is not present in the DTO set.
        var itemsToRemove = entities.Where(e => leftKey(e) != Guid.Empty && !dtoIds.Contains(leftKey(e))).ToArray();

        foreach (var item in itemsToRemove)
        {
            entities.Remove(item);
        }
    }
}