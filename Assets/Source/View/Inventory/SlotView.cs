using Model.BlocksLogic.BlocksData;
using UnityEngine;
using View.Blocks.UI;
using View.InventoryLogic.AmountLogic;
using UnityEngine.EventSystems;
using Veiw.InventoryLogic.AmountLogic;
using Model.InventoryLogic.AmountLogic;
using View.Highlighters;
using System;

namespace View.InventoryLogic
{
    public class SlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private BlockUIViewFactory _blockViewFactory;
        [SerializeField] private AmountViewFactory _amountViewFactory;
        [SerializeField] private ComponentHighlighter _highlighter;
        [SerializeField] private RectTransform _blockContainer;
        [SerializeField] private RectTransform _amountContainer;

        private IBlockData _data;

        public event Action OnSlotClick;

        public IHighlighter Highlighter => _highlighter;
        public IBlockData Data => _data;

        private void InitBlock()
        {
            BlockUIView blockView = _data.AcceptFactory(_blockViewFactory);
            blockView.transform.SetParent(_blockContainer, false);
        }

        private void InitAmount(IReadOnlyAmount amount)
        {
            AmountView amountView = amount.AcceptFactory(_amountViewFactory);
            amountView.transform.SetParent(_amountContainer, false);
        }

        public void Init(IBlockData data, IReadOnlyAmount amount)
        {
            _data = data;

            InitBlock();
            InitAmount(amount);
        }

        public void OnPointerClick(PointerEventData eventData)
            => OnSlotClick?.Invoke();
    }
}