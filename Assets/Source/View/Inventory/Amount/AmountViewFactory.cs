using UnityEngine;
using Model.InventoryLogic.AmountLogic;
using View.InventoryLogic.AmountLogic;

namespace Veiw.InventoryLogic.AmountLogic
{
    [CreateAssetMenu(fileName = "AmountViewFactory", menuName = "View/AmountViewFactory", order = 51)]
    public class AmountViewFactory : ScriptableObject, IAmountBasedFactory<AmountView>
    {
        [SerializeField] private ValueAmountView _valueAmountView;
        [SerializeField] private AmountView _infinityAmountView;

        public AmountView Create(ValueAmount amount)
        {
            ValueAmountView view = Instantiate(_valueAmountView);
            view.Init(amount);
            return view;
        }

        public AmountView Create(InfinityAmount amount)
            => Instantiate(_infinityAmountView);
    }
}