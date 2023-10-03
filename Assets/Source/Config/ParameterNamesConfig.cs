using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using UnityEngine;

namespace Config
{

    [CreateAssetMenu(fileName = "Parameters Names Config", menuName = "Config/Parameters Names", order = 51)]
    public class ParameterNamesConfig : ScriptableObject
    {
        [Serializable] private class ParameterConfig
        {
            public int Id;
            public string Name;
        }

        private const string PARAMETER_DEFAULT_NAME = "?";

        [SerializeField] private ParameterConfig[] _parameters;

        private Dictionary<int, string> _parametersDictionary = null;

        public IEnumerable<int> ParametersId 
            => _parameters.Select(p => p.Id);
        
        public int NumberOfParameters
            => _parameters.Count(); 

        public string this[int id]
        {
            get
            {
                if(_parametersDictionary == null)
                    _parametersDictionary = _parameters.ToDictionary(p => p.Id, p => p.Name);
                if(_parametersDictionary.TryGetValue(id, out string name))
                    return name;
                return PARAMETER_DEFAULT_NAME;
            }
        }
    }
}