using Configs.LevelConfigs;
using Extensions;
using Model.BlocksLogic.BlocksData;
using Model.BlocksLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using System.Linq;
using System;
using UnityEngine;

namespace Presenter.Builder
{
    public class PlacingPresenter : BuilderPresenterState
    {
        private readonly ReadOnlyMap _map;
        private readonly Inventory _inventory;
        private readonly Vector2Int _rootPosition;

        private IBlockData _currentData;
        private bool _isDataSelected;

        public event Action<IBlockData> OnDataSelected;

        public IBlockData CurrentData
        {
            get => _currentData;
            set
            {
                _currentData = value;
                _isDataSelected = true;
                OnDataSelected?.Invoke(value);
            }
        }

        private bool PositionIsNotOccupied(Vector2Int position)
            => _map[position].IsOccupied == false;

        private bool ExistOnlyOneParent(Vector2Int at)
        {
            bool isOneParentFound = false;

            MapVicinity vicinity = _map.GetVicinity(at);
            foreach(var (fromCenter, position) in vicinity.Positions.Where(p => _map[p.Value].IsOccupied))
            {
                IReadOnlyBlock block = _map[position].Block;
                Direction toCenter = fromCenter.Reverse();
                if(block.IsAppendCorrect(toCenter))
                {
                    if(isOneParentFound)
                        return false;
                    isOneParentFound = true;
                }
            }

            return isOneParentFound;
        }

        public PlacingPresenter(ReadOnlyMap map, Inventory inventory, TreeConfig treeConfig)
        {
            _map = map;
            _inventory = inventory;
            _rootPosition = treeConfig.RootPosition;

            _isDataSelected = false;
        }

        public override bool IsPositionCorrect(Vector2Int position)
            => PositionIsNotOccupied(position) && (position == _rootPosition || ExistOnlyOneParent(position));

        public override bool TryExecute(Vector2Int position)
        {
            if(_isDataSelected
            && PositionIsNotOccupied(position) 
            && (position == _rootPosition || ExistOnlyOneParent(position))
            && _inventory.CanPullOut(_currentData, position))
            {
                _inventory.PullOut(_currentData, position);
                return true;
            }

            return false;
        }

        public override void Exit()
        {
            base.Exit();
            _isDataSelected = false;
        }
    }
}