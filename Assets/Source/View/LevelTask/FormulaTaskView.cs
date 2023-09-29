using Model.LevelTaskLogic;
using TMPro;
using UnityEngine;

namespace View.LevelTaskLogic
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