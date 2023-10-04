using Configs.LevelConfigs;
using Model.BlocksLogic.BlocksData;
using Model.InventoryLogic;
using Model.MapLogic;
using System.Linq;
using System;
using UnityEngine;
using Model.BlocksLogic;
using Extensions;
using Converter;
using System.Net.Http.Headers;

namespace Presenter.Builder
{
    public class PlacingPresenter : BuilderPresenterState
    {
        private readonly Map _map;
        private readonly Inventory _inventory;
        private readonly Vector2Int _rootPosition;
        private readonly IConverter<Vector2Int, Direction> _vectorToDirection;

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

        private bool ExistOnlyOneParent(Vector2Int position)
        {
            bool isOneParentFound = false;

            foreach(Vector2Int vicinityPosition in _map.GetVicinity(position).Where(p => _map[p].IsOccupied))
            {
                Block block = _map[vicinityPosition].Block;
                Direction toChild = _vectorToDirection.Convert(position - vicinityPosition);
                if(block.IsAppendCorrect(toChild))
                {
                    if(isOneParentFound)
                        return false;
                    isOneParentFound = true;
                }
            }

            return isOneParentFound;
        }

        public PlacingPresenter(Map map, Inventory inventory, TreeConfig treeConfig, IConverter<Vector2Int, Direction> vectorToDirection)
        {
            _map = map;
            _inventory = inventory;
            _rootPosition = treeConfig.RootPosition;
            _vectorToDirection = vectorToDirection;

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