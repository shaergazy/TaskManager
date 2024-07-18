using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO.Base;

public sealed record BadRequestDto
{
    [JsonPropertyName("errors")]
    public IDictionary<string, IEnumerable<string>> Errors { get; init; }

    [JsonPropertyName("devCode")]
    public int? DevCode { get; init; }
}
