using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Config
{

    [CreateAssetMenu(fileName = "Parameters Config", menuName = "Config/Parameters Config", order = 51)]
    public class ParametersConfig : ScriptableObject
    {
        [Serializable] private class ParameterConfig
        {
            public int Id;
            public string Name;
        }

        [SerializeField] private ParameterConfig[] _parameters;

        public IEnumerable<int> ParametersId 
            => _parameters.Select(p => p.Id);
        
        public int NumberOfParameters
            => _parameters.Count(); 

        public Dictionary<int, string> ToNameDictionary()
            => _parameters.ToDictionary(p => p.Id, p => p.Name);
    }
}