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

    public class CompanyIdConverter : JsonConverter<CompanyId>
    {
        public override void WriteJson(JsonWriter writer, CompanyId value, JsonSerializer serializer) =>
            writer.WriteValue(value);

        public override CompanyId ReadJson(JsonReader reader, Type objectType, CompanyId existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            (CompanyId)(string)reader.Value;
    }
}