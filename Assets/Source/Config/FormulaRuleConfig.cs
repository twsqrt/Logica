using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "FormulaRuleConfig", menuName = "Config/Rule/FormulaRuleConfig", order = 51)]
    public class FormulaRuleConfig : ScriptableObject
    {
        [SerializeField] private string _parseString;

        public string ParseString => _parseString;
    }
    }