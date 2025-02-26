namespace AppShareOn.Core.Entities;

/// <summary>
/// Entities that can be updated or deleted.
/// </summary>
public class UpdatableEntity : BaseEntity
{
    /// <summary>
    /// Date-time for when the entity was last updated.
    /// </summary>
    public DateTime? UpdatedDateTime { get; set; }

    /// <summary>
    /// Date-time for when the entity was deleted.
    /// </summary>
    public DateTime? DeletedDateTime { get; set; }
}