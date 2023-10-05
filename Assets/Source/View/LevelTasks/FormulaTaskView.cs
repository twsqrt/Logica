using Model.LevelTasksLogic;
using TMPro;
using UnityEngine;

namespace View.LevelTasksLogic
{
    public class FormulaTaskView : LevelTaskView
    {
        [SerializeField] private TextMeshProUGUI _formulaText;

        public void Init(FormulaTask task)
        {
            _formulaText.text= task.TaskConfig.ViewText;
        }
    }
}