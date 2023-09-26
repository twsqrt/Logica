using Model;
using TMPro;
using UnityEngine;

namespace View.LevelTaskLogic
{
    public class FormulaTaskView : LevelTaskView
    {
        [SerializeField] private TextMeshProUGUI _tmp;

        public void Init(FormulaTask task)
        {
            _tmp.text = "Formula Task Template";
        }
    }
}