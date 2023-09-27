using Model.LevelTaskLogic;
using TMPro;
using UnityEngine;

namespace View.LevelTaskLogic.AmountTaskLogic
{
    public class AmountTaskView : LevelTaskView
    {
        [SerializeField] private AmountConditionView _conditionPrefab;
        [SerializeField] private Transform _container;
        
        public void Init(AmountTask task)
        {
            foreach(var (blockData, limit) in task.AmountLimits)
                Instantiate(_conditionPrefab, _container).Init(blockData, limit);
        }
    }
}