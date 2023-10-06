using Model.LevelTasksLogic;
using View.LevelTasksLogic.AmountSaveTaskLogic;
using UnityEngine;
using Mappers;
using Zenject;

namespace View.LevelTasksLogic
{
    [CreateAssetMenu(fileName = "Level Task View Factory", menuName = "Factory/Level Task View", order = 51)]
    public class LevelTaskViewFactory : ScriptableObject, ILevelTaskVisitor<LevelTaskView>
    {
        [SerializeField] private FormulaTaskView _formulataTaskPrefab;
        [SerializeField] private AmountSaveTaskView _amountSaveTaskPrefab;

        public LevelTaskView Visit(FormulaTask formulaTask)
        {
            FormulaTaskView view =  Instantiate(_formulataTaskPrefab);
            view.Init(formulaTask.TaskConfig);
            return view;
        }

        public LevelTaskView Visit(AmountSaveTask amountSaveTask)
        {
            AmountSaveTaskView view =  Instantiate(_amountSaveTaskPrefab);
            view.Init(amountSaveTask);
            return view;
        }
    }
}