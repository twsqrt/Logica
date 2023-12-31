using Model.LevelTasksLogic;
using UnityEngine;
using View.Blocks.UI;

namespace View.LevelTasksLogic.AmountSaveTaskLogic
{
    public class AmountSaveTaskView : LevelTaskView
    {
        [SerializeField] private BlockUIViewFactory _blockViewFactory;
        [SerializeField] private AmountConditionView _conditionPrefab;
        [SerializeField] private Transform _container;
        
        public void Init(AmountSaveTask task)
        {
            foreach(var (blockData, limit) in task.AmountLimits)
                Instantiate(_conditionPrefab, _container).Init(_blockViewFactory, blockData, limit);
        }
    }
}