using Model.LevelTaskLogic;
using TMPro;
using UnityEngine;

namespace View.LevelTaskLogic
{
    public class AmountTaskView : LevelTaskView
    {
        [SerializeField] private TextMeshProUGUI _tmp;

        public void Init(AmountTask task)
        {
            _tmp.text = "Amount Task Template";
        }
    }
}