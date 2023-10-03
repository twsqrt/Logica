using System;
using UnityEngine;
using Model.BlockLogic;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace Config.LevelLogic.JsonConverter
{
    public class BlockDataConverter : JsonConverter<IBlockData>
    {
        public override bool CanRead => true;

        private IBlockData ParseFromJObject(JObject jo)
        {
            BlockType blockType = jo["blockType"].ToObject<BlockType>();
            if((blockType & BlockType.LOGIC_OPERATION) != 0)
            {
                return new OperationData((LogicOperationType)blockType);
            }
            else if(blockType == BlockType.PARAMETER)
            {
                int id = jo["id"].ToObject<int>();
                return new ParameterData(id);
            }

            throw new ArgumentException();
        }

        public override IBlockData ReadJson(JsonReader reader, Type objectType, IBlockData existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.StartObject)
            {
                JObject jo = JObject.Load(reader);
                return ParseFromJObject(jo);
            }
            else if(reader.TokenType == JsonToken.String)
            {
                string typeString = (string)reader.Value;
                BlockType blockType = Enum.Parse<BlockType>(typeString);
                if((blockType & BlockType.LOGIC_OPERATION) != 0)
                    return new OperationData((LogicOperationType)blockType);
            }

            throw new ArgumentException();
        }

        public override void WriteJson(JsonWriter writer, IBlockData value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}