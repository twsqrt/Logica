using Configs.LevelConfigs.LevelTasksConfigs;
using Mappers;
using TMPro;
using UnityEngine;
using Zenject;

namespace View.LevelTasksLogic
{
    public class FormulaTaskView : LevelTaskView
    {
        [SerializeField] private TextMeshProUGUI _formulaText;
        [SerializeField] private FormulaParser _parser;

        [Inject] public void Init(FormulaTaskConfig taskConfig)
        {
            _formulaText.text = _parser.Parse(taskConfig.FormulaText);
        }
    }
}