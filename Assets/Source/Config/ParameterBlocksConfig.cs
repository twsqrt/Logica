using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ParameterBlocksConfig", menuName = "Config/ParameterBlocksConfig", order = 51)]
    public class ParameterBlocksConfig : ScriptableObject
    {
        [Serializable]
        private class ParameterInfo
        {
            public int Id;
            public string Name; 
        }

        [SerializeField] private List<ParameterInfo> _names;

        public Dictionary<int, string> GetParameterNameByIdDictionary()
            => _names.ToDictionary(p => p.Id, p => p.Name);
    }
}