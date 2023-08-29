using Model.InventoryLogic.AmountLogic;
using TMPro;
using UnityEngine;

namespace View.InventoryLogic.AmountLogic
{
    public class ValueAmountView : AmountView
    {
        [SerializeField] private TextMeshPro _tmp;

        private void UpdateAmountText(int value)
            => _tmp.text = value.ToString();

        public void Init(ValueAmount amount)
        {
            UpdateAmountText(amount.Value);
            amount.OnValueChange += UpdateAmountText;
        }
    }
}