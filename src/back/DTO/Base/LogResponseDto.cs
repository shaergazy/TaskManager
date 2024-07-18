namespace DTO.Base;

/// <summary>
/// 
/// </summary>
public readonly record struct LogResponseDto
{
    /// <summary>
    /// 
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string UserName { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string BodyJson { get; init; }
}
