using UnityEngine;
using Model.InventoryLogic.AmountLogic;
using View.InventoryLogic.AmountLogic;

namespace Veiw.InventoryLogic.AmountLogic
{
    [CreateAssetMenu(fileName = "Amount View Factory", menuName = "Factory/Amount View", order = 51)]
    public class AmountViewFactory : ScriptableObject, IAmountBasedFactory<AmountView>
    {
        [SerializeField] private ValueAmountView _valueAmountView;
        [SerializeField] private AmountView _infinityAmountView;

        public AmountView Create(IReadOnlyValueAmount amount)
        {
            ValueAmountView view = Instantiate(_valueAmountView);
            view.Init(amount);
            return view;
        }

        public AmountView Create(InfinityAmount amount)
            => Instantiate(_infinityAmountView);
    }
}