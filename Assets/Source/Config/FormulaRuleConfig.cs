using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Formula Rule Config", menuName = "Config/Rule/Formula Rule", order = 51)]
    public class FormulaRuleConfig : ScriptableObject
    {
        [SerializeField] private string _parseString;

        public string ParseString => _parseString;
    }
    }