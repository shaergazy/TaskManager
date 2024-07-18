using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Common.Helpers;
using Refit;

namespace BLL.Services.External
{
    internal sealed class Utf8ContentSerializer : IHttpContentSerializer
    {
        private Utf8Json.IJsonFormatterResolver Resolver { get; }

        public Utf8ContentSerializer()
        {
            Resolver = Utf8Json.Resolvers.StandardResolver.ExcludeNullCamelCase;
        }

        public HttpContent ToHttpContent<T>(T payload)
        {
            var stream = new MemoryStream();
            Utf8Json.JsonSerializer.NonGeneric.Serialize(stream, payload, Resolver);
            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var content = new StreamContent(stream);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(AppConstants.AppJsonContentType);

            return content;
        }

        public async Task<T> FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)
        {
            var body = await content.ReadAsStreamAsync(cancellationToken);
            return (T)await Utf8Json.JsonSerializer.NonGeneric.DeserializeAsync(typeof(T), body, Resolver);
        }

        public string GetFieldNameForProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            return propertyInfo.GetCustomAttributes<DataMemberAttribute>(true)
                .Select(x => x.Name)
                .FirstOrDefault();
        }
    }
}
