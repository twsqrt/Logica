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

        private void OnSlotClickHandler(IBlockData data)
            => _builder.SelectData(data);

        public void Init(BuilderPresenter builder, Inventory inventory)
        {
            _builder = builder;
            _slots = new Dictionary<IBlockData, SlotView>();

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