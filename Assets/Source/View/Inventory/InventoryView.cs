using Model.BlocksLogic.BlocksData;
using Model.InventoryLogic;
using Presenter.Builder;
using UnityEngine;
using Zenject;

namespace View.InventoryLogic
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private SlotView _slotPrefab;
        [SerializeField] private Transform _slotContainer;

        private BuilderPresenter _builderPresenter;
        private PlacingPresenter _placingPresenter;
        private SlotView _currentSlot;

        private void SelectSlot(SlotView slot)
        {
            _currentSlot?.Highlighter.HighlightDisable();
            _currentSlot = slot;
            _currentSlot.Highlighter.HighlightEnable();

            _placingPresenter.CurrentData = slot.Data;

            if(_builderPresenter.Mode != BuildingMode.PLACING)
                _builderPresenter.SetMode(BuildingMode.PLACING);
        }

        private void UnselectCurrentSlot()
        {
            _currentSlot?.Highlighter.HighlightDisable();
            _currentSlot = null;
        }

        [Inject] private void Init(
            IReadOnlyInventory inventory, 
            BuilderPresenter builderPresenter, 
            PlacingPresenter placingPresenter)
        {
            _builderPresenter = builderPresenter;
            _placingPresenter = placingPresenter;
            _placingPresenter.OnExit += UnselectCurrentSlot;

            _currentSlot = null;

            foreach(IBlockData blockData in inventory.AllBlocksData)
            {
                SlotView view = Instantiate(_slotPrefab, _slotContainer);
                view.Init(blockData, inventory[blockData]);
                view.OnSlotClick += () => SelectSlot(view);
            }
        }
    }
}