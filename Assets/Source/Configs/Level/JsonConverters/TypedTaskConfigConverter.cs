using System;
using Configs.LevelConfigs.LevelTasksConfigs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Configs.LevelConfigs.JsonConverters
{
    public class TypedTaskConfigConverter : JsonConverter<TypedTaskConfig>
    {
        public override bool CanRead
            => true;

        public override TypedTaskConfig ReadJson(JsonReader reader, Type objectType, TypedTaskConfig existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            LevelTaskType type = jo["taskType"].ToObject<LevelTaskType>();

            ITaskConfig taskConfig =  type switch
            {
                LevelTaskType.FORMULA => jo.ToObject<FormulaTaskConfig>(),
                LevelTaskType.AMOUNT_SAVE => jo.ToObject<AmountSaveTaskConfig>(),
                LevelTaskType.RECTANGULAR_AREA => jo.ToObject<RectangularAreaTaskConfig>(),
                _ => throw new ArgumentException("Level task type not found!"),
            };

            return new TypedTaskConfig(taskConfig);
        }

        public override void WriteJson(JsonWriter writer, TypedTaskConfig value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}