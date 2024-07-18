using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Helpers
{
    public class UriHelper
    {
        public static Dictionary<string, string> DecodeQueryParameters(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentNullException("uri");

            //var startIndex = uri.Contains("?") ? uri.IndexOf('?') : 0;
            //return uri.Substring(startIndex).TrimStart('?')
            return uri.TrimStart('?')
                .Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(parts => parts[0],
                    parts => parts.Length > 2 ? string.Join("=", parts, 1, parts.Length - 1) : (parts.Length > 1 ? parts[1] : ""))
                .ToDictionary(grouping => grouping.Key,
                    grouping => string.Join(",", grouping));
        }
    }
}
