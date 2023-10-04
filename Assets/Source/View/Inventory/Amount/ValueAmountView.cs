using Model.InventoryLogic.AmountLogic;
using TMPro;
using UnityEngine;

namespace View.InventoryLogic.AmountLogic
{
    public class ValueAmountView : AmountView
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _zeroColor;

        private void UpdateAmountText(int value)
        {
            _valueText.color = value != 0 ? _defaultColor : _zeroColor;
            _valueText.text = value.ToString();
        }

        public void Init(IReadOnlyValueAmount amount)
        {
            UpdateAmountText(amount.Value);
            amount.OnValueChange += UpdateAmountText;
        }
    }
}