using Model.LevelTasksLogic;
using View.LevelTasksLogic.AmountTaskLogic;
using UnityEngine;
using Mappers;
using Zenject;

namespace View.LevelTasksLogic
{
    [CreateAssetMenu(fileName = "Level Task View Factory", menuName = "Factory/Level Task View", order = 51)]
    public class LevelTaskViewFactory : ScriptableObject, ILevelTaskVisitor<LevelTaskView>
    {
        [SerializeField] private FormulaTaskView _formulataTaskPrefab;
        [SerializeField] private AmountTaskView _amountTaksPrefab;

        private FormulaMapper _formulaMapper;

        //template solution
        public void Init(FormulaMapper formulaMapper)
        {
            _formulaMapper = formulaMapper;
        }

        public LevelTaskView Visit(FormulaTask formulaTask)
        {
            FormulaTaskView view =  Instantiate(_formulataTaskPrefab);
            view.Init(formulaTask.TaskConfig, _formulaMapper);
            return view;
        }

        public LevelTaskView Visit(AmountTask amountTask)
        {
            AmountTaskView view =  Instantiate(_amountTaksPrefab);
            view.Init(amountTask);
            return view;
        }
    }
}