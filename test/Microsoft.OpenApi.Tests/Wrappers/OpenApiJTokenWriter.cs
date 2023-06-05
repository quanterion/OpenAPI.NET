using System;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json.Linq;

namespace Microsoft.OpenApi.Tests.Wrappers
{
    public class OpenApiJTokenWriter : IOpenApiWriter
    {
        readonly JTokenWriter writer = new JTokenWriter();

        public JToken Token
            => this.writer.Token;

        public void Flush()
            => this.writer.Flush();

        public void WriteEndArray()
            => this.writer.WriteEndArray();

        public void WriteEndObject()
            => this.writer.WriteEndObject();

        public void WriteNull()
            => this.writer.WriteNull();

        public void WritePropertyName(string name)
            => this.writer.WritePropertyName(name);

        public void WriteRaw(string value)
            => throw new InvalidOperationException();

        public void WriteStartArray()
            => this.writer.WriteStartArray();

        public void WriteStartObject()
            => this.writer.WriteStartObject();

        public void WriteValue(string value)
            => this.writer.WriteValue(value);

        public void WriteValue(long value)
            => this.writer.WriteValue(value);
        
        public void WriteValue(decimal value)
            => this.writer.WriteValue(value);

        public void WriteValue(int value)
            => this.writer.WriteValue(value);

        public void WriteValue(bool value)
            => this.writer.WriteValue(value);

        /// <summary>
        /// Writes date-time format according to openapi spec
        /// https://swagger.io/docs/specification/data-models/data-types/
        /// https://datatracker.ietf.org/doc/html/rfc3339#section-5.6
        /// for example, 2017-07-21
        /// </summary>
        /// <param name="value"></param>
        public void WriteValue(DateTime value)
            => this.WriteValue(value.ToString("yyyy-MM-dd"));

        /// <summary>
        /// Writes date-time format according to openapi spec
        /// https://swagger.io/docs/specification/data-models/data-types/
        /// https://datatracker.ietf.org/doc/html/rfc3339#section-5.6
        /// for example, 2017-07-21T17:32:28Z
        /// </summary>
        /// <param name="value"></param>
        public void WriteValue(DateTimeOffset value)
            => this.WriteValue(value.ToString("yyyy-MM-ddThh:mm:ss"));

        public void WriteValue(object value)
            => this.writer.WriteValue(value);
    }
}
