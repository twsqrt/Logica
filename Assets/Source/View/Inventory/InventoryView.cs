using Model.BlockLogic.BlockDataLogic;
using Model.InventoryLogic;
using Presenter.BuilderLogic;
using System.Collections.Generic;
using UnityEngine;

namespace View.InventoryLogic
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private SlotView _slotPrefab;
        [SerializeField] private Transform _slotContainer;

        private BuilderPresenter _builder;
        private Dictionary<IBlockData, SlotView> _slots;
        private SlotView _currentSlot;

        private void OnSlotClickHandler(IBlockData data)
        {
            if(_slots.TryGetValue(data, out SlotView slot))
            {
                _currentSlot?.Highlighter.HighlightDisable();
                _currentSlot = slot;
                slot.Highlighter.HighlightEnable();

                _builder.SelectData(data);
            }
        }

        public void Init(BuilderPresenter builder, Inventory inventory)
        {
            _builder = builder;

            _slots = new Dictionary<IBlockData, SlotView>();
            _currentSlot = null;

            foreach(IBlockData blockData in inventory.AllBlocksData)
            {
                SlotView view = Instantiate(_slotPrefab, _slotContainer);
                view.Init(blockData, inventory[blockData]);
                view.OnSlotClick += OnSlotClickHandler;

                _slots.Add(blockData, view);
            }
        }
    }
}