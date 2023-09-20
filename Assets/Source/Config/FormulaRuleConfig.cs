using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "FormulaRuleConfig", menuName = "Config/Rule/FormulaRuleConfig", order = 51)]
    public class FormulaRuleConfig : ScriptableObject
    {
        [SerializeField] private string _viewString;
        [SerializeField] private string _compilingString;

        public string ViewString => _viewString;
        public string CompilingString => _compilingString;
    }
}