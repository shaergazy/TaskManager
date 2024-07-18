using Common.Interfaces;

namespace DTO.Base;

/// <summary>
/// 
/// </summary>
public sealed record EnumDto : IEnum
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; init; }
}
