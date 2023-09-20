using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ParametersConfig", menuName = "Config/ParametersConfig", order = 51)]
    public class ParametersConfig : ScriptableObject
    {
        [Serializable]
        private class ParameterInfo
        {
            public int Id;
            public string Name; 
        }

        [SerializeField] private List<ParameterInfo> _names;

        public int NumberOfParameters => _names.Count();

        public IEnumerable<int> GetParametersId()
            => _names.Select(p => p.Id);

        public Dictionary<int, string> GetParameterNameByIdDictionary()
            => _names.ToDictionary(p => p.Id, p => p.Name);
    }
}