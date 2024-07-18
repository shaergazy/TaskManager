namespace DTO.Base;

public readonly record struct LogRequestDto
{
    public string Method { get; init; }

    public string Path { get; init; }

    public string Query { get; init; }

    public string UserAgent { get; init; }

    public string Authorization { get; init; }

    public string Culture { get; init; }

    public string BodyJson { get; init; }
}
