using System;
using Newtonsoft.Json;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services.Serialization
{
    public class RawIdConverter : JsonConverter<RawId>
    {
        public override void WriteJson(JsonWriter writer, RawId value, JsonSerializer serializer) =>
            writer.WriteValue(value);

        public override RawId ReadJson(JsonReader reader, Type objectType, RawId existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            (RawId)(string)reader.Value;
    }
}