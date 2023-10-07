using Model.LevelTasksLogic;
using TMPro;
using UnityEngine;

namespace View.LevelTasksLogic
{
    public class RectangularAreaTaskView : LevelTaskView
    {
        [SerializeField] private TextMeshProUGUI _areaLimitText;
        [SerializeField] private string _areaLimitTextTag;

        public void Init(RectangularAreaTask task)
        {
            string rawText = _areaLimitText.text;
            _areaLimitText.text = rawText.Replace(_areaLimitTextTag, task.AreaLimit.ToString());
        }
    }
}