using Newtonsoft.Json;

namespace EntityApp.Dal
{
    public class GuidConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(Guid));
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            try
            {
                return serializer.Deserialize<Guid>(reader);
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Идентификатор объекта (Id) должен быть в формате GUID", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
