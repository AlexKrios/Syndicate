using System;
using Newtonsoft.Json;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services.Serialization
{
    public class RawIdConverter : JsonConverter<RawItemId>
    {
        public override void WriteJson(JsonWriter writer, RawItemId value, JsonSerializer serializer) =>
            writer.WriteValue(value);

        public override RawItemId ReadJson(JsonReader reader, Type objectType, RawItemId existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            (RawItemId)(string)reader.Value;
    }
}