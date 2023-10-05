using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs
{

    [CreateAssetMenu(fileName = "Parameters Names Configs", menuName = "Configs/Parameters Names", order = 51)]
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

        public Dictionary<int, string> ToDictionary()
            => _parameters.ToDictionary(p => p.Id, p => p.Name);
        
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