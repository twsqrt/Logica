using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ParameterConfig", menuName = "Config/ParameterConfig", order = 51)]
    public class ParameterConfig : ScriptableObject
    {
        [Serializable]
        private class ParameterInfo
        {
            public int Id;
            public string Name; 
        }

        [SerializeField] private List<ParameterInfo> _names;

        public Dictionary<int, string> ToDictionary()
            => _names.ToDictionary(n => n.Id, n => n.Name);
    }
}