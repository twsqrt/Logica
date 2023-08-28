using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace config
{
    [CreateAssetMenu(fileName = "ParameterConfig", menuName = "Config/ParameterConfig", order = 51)]
    public class ParameterConfig : ScriptableObject
    {
        [Serializable]
        private class ParameterName
        {
            public int Id;
            public string Name; 
        }

        [SerializeField] private List<ParameterName> _names;

        public Dictionary<int, string> ToDictionary()
            => _names.ToDictionary(n => n.Id, n => n.Name);
    }
}