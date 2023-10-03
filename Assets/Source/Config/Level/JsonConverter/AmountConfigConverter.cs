using System;
using Newtonsoft.Json;
using Config.LevelLogic.InventoryLogic;

namespace Config.LevelLogic.JsonConverterLogic
{
    public class AmountConfigConverter : JsonConverter<AmountConfig>
    {
        public override bool CanRead => true;

        public override AmountConfig ReadJson(JsonReader reader, Type objectType, AmountConfig existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JsonToken token = reader.TokenType;
            if(token == JsonToken.Integer)
                return AmountConfig.CreateValue(Convert.ToInt32(reader.Value));
            else if(token == JsonToken.String && (string)reader.Value == "INF")
                return AmountConfig.CreateInfinity();
            
            throw new ArgumentException();
        }

        public override void WriteJson(JsonWriter writer, AmountConfig value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}