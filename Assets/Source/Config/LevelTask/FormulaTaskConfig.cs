using UnityEngine;

namespace Config.LevelTaskLogic
{
    [CreateAssetMenu(fileName = "Formula Task Config", menuName = "Config/Task/Formula Task", order = 51)]
    public class FormulaTaskConfig : ScriptableObject
    {
        [SerializeField] private string _viewString;
        [SerializeField] private string _parseString;

        public string ViewString => _viewString;
        public string ParseString => _parseString;
    }
    }