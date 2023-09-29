using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Config.ParametersLogic
{
    public class ParametersConfig
    {
        [JsonProperty("parameters")] private ParameterConfig[] _parameters;

        public int NumberOfParameters 
            => _parameters.Count();
        
        public IEnumerable<int> ParametersId
            => _parameters.Select(p => p.Id);

        public Dictionary<int, string> ToNameDictionary()
            => _parameters.ToDictionary(p => p.Id, p => p.Name);
    }
}