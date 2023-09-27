using Model.LevelTaskLogic;
using View.LevelTaskLogic.AmountTaskLogic;
using UnityEngine   ;

namespace View.LevelTaskLogic
{
    [CreateAssetMenu(fileName = "Level Task View Factory", menuName = "Factory/Level Task View", order = 51)]
    public class LevelTaskViewFactory : ScriptableObject, ILevelTaskVisitor<LevelTaskView>
    {
        [SerializeField] private FormulaTaskView _formulataTaskPrefab;
        [SerializeField] private AmountTaskView _amountTaksPrefab;

        public LevelTaskView Visit(FormulaTask formulaTask)
        {
            FormulaTaskView view =  Instantiate(_formulataTaskPrefab);
            view.Init(formulaTask);
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