using Configs.LevelConfigs.LevelTasksConfigs;
using Mappers;
using TMPro;
using UnityEngine;

namespace View.LevelTasksLogic
{
    public class FormulaTaskView : LevelTaskView
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        public void Init(FormulaTaskConfig taskConfig, FormulaMapper mapper)
        {
            _formulaText.text= mapper.From(taskConfig.FormulaText);
        }
    }
}