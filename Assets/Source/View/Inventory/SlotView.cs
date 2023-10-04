using Model.BlocksLogic.BlocksData;
using UnityEngine;
using View.BlockLogic;
using View.InventoryLogic.AmountLogic;
using UnityEngine.EventSystems;
using Veiw.InventoryLogic.AmountLogic;
using Model.InventoryLogic.AmountLogic;
using View.HighlighterLogic;
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

        public event Action<IBlockData> OnSlotClick;

        public IHighlighter Highlighter => _highlighter;

        private void InitBlock(IBlockData data)
        {
            BlockUIView blockView = data.AcceptFactory(_blockViewFactory);
            blockView.transform.SetParent(_blockContainer, false);
        }

        private void InitAmount(IAmount amount)
        {
            AmountView amountView = amount.AcceptFactory(_amountViewFactory);
            amountView.transform.SetParent(_amountContainer, false);
        }

        public void Init(IBlockData data, IAmount amount)
        {
            _data = data;

            InitBlock(data);
            InitAmount(amount);
        }

        public void OnPointerClick(PointerEventData eventData)
            => OnSlotClick?.Invoke(_data);
    }
}