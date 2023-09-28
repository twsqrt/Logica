using System.Collections.Generic;
using System.Linq;

namespace Config
{
    public class ParametersConfig
    {
        private readonly IReadOnlyCollection<ParameterConfig> _parameters;

        public int NumberOfParameters 
            => _parameters.Count();
        
        public IEnumerable<int> ParametersId
            => _parameters.Select(p => p.Id);

        public ParametersConfig(IReadOnlyCollection<ParameterConfig> parameters)
        {
            _parameters = parameters;
        }

        public Dictionary<int, string> ToNameDictionary()
            => _parameters.ToDictionary(p => p.Id, p => p.Name);
    }
}