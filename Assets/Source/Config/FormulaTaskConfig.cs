using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Formula Task Config", menuName = "Config/Task/Formula Task", order = 51)]
    public class FormulaTaskConfig : ScriptableObject
    {
        [SerializeField] private string _parseString;

        public string ParseString => _parseString;
    }
    }