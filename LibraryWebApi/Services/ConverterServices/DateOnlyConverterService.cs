using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryWebApi.Services.ConverterServices
{
    public class DateOnlyConverterService : JsonConverter<DateOnly>

    {
        private readonly string serializationFormat;

        public DateOnlyConverterService() : this(null)
        {
        }

        public DateOnlyConverterService(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(serializationFormat));
        }
    }
}