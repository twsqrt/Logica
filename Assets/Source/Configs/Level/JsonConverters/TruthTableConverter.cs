using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Configs.LevelConfigs.LevelTasksConfigs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Configs.LevelConfigs.JsonConverters
{
    public class TruthTableConverter : JsonConverter<TruthTable>
    {
        public override bool CanRead 
            => true;

        private bool DigitBoolParse(string line)
        => line switch
        {
            "0" => false,
            "1" => true,
            _ => throw new ArgumentException()
        };
        
        private void AddLine(string line, Dictionary<IEnumerable<bool>, bool> table)
        {
            string[] words = line.Split("=>");
            if(words.Length != 2)
                throw new ArgumentException();

            IEnumerable<bool> parameters = words[0].Split(',').Select(p => DigitBoolParse(p));
            bool result = DigitBoolParse(words[1]);

            table.Add(parameters, result);
        }

        public override TruthTable ReadJson(JsonReader reader, Type objectType, TruthTable existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JArray ja = JArray.Load(reader);
            IEnumerable<string> lines = ja.Select(j => j.ToString());

            var table = new Dictionary<IEnumerable<bool>, bool>();
            foreach(string line in lines)
                AddLine(line.Replace(" ", string.Empty), table);

            return new TruthTable(table);
        }

        public override void WriteJson(JsonWriter writer, TruthTable value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}