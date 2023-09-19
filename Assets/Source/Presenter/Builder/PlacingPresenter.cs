using Extensions;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro.EditorUtilities;

namespace Presenter.BuilderLogic
{
    public class PlacingPresenter : BuilderPresenterState
    {
        private readonly Map _map;
        private readonly Inventory _inventory;

        private IBlockData _currentData;
        private Block _root;
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

        private bool IsRootPlacement(Vector2Int position)
            => _root == null && position == _map.ExecutionPosition;
        
        private bool ExistOnlyOneParent(Vector2Int position, out Vector2Int parentPosition)
        {
            parentPosition = Vector2Int.zero;

            bool isPositionAlreadySelected = false;
            foreach(Vector2Int vicinityPosition in _map.GetVicinity(position).Where(p => _map[p].IsOccupied))
            {
                BlockSide toVicinityCenter = BlockSideMapper.BlockSideFromParentPosition(vicinityPosition, position);
                if(_map[vicinityPosition].Block.IsAppendCorrect(toVicinityCenter))
                {
                    if(isPositionAlreadySelected)
                        return false;

                    isPositionAlreadySelected = true;
                    parentPosition = vicinityPosition;
                }
            }

            return isPositionAlreadySelected;
        }

        private bool TryExecuteForRoot(IBlockData _currentData)
        {
            BlockContext context = BlockContext.CreateRootContext();
            if(_inventory.TryPullOut(_currentData, context, out Block root) == false)
                return false;

            _root = root;
            _map[_map.ExecutionPosition].TryPlace(root);
            return true;
        }

        private bool CanPlace(Vector2Int position)
            => _map.PositionInMap(position) && _map[position].IsOccupied == false;

        public PlacingPresenter(Map map, Inventory inventory)
        {
            _map = map;
            _inventory = inventory;

            _isDataSelected = false;
        }

        public override IEnumerable<Vector2Int> GetCorrectPositions(IEnumerable<Vector2Int> positions)
            => positions.Where(p => CanPlace(p) && ( IsRootPlacement(p) || ExistOnlyOneParent(p, out _)));

        public override bool TryExecute(Vector2Int position)
        {
            if(_isDataSelected == false || CanPlace(position) == false)
                return false;

            if(IsRootPlacement(position))
                return TryExecuteForRoot(_currentData);

            if(ExistOnlyOneParent(position, out Vector2Int parentPosition) == false)
                return false;

            BlockSide fromChildToParent = BlockSideMapper.BlockSideFromParentPosition(position, parentPosition);
            BlockContext context = BlockContext.CreateChildContext(fromChildToParent);
            if(_inventory.TryPullOut(_currentData, context, out Block childBlock) == false)
                return false;

            Block parent = _map[parentPosition].Block;
            parent.Append(fromChildToParent.Reverse(), childBlock);
            _map[position].TryPlace(childBlock);

            return true;
        }

        public override void Exit()
        {
            base.Exit();
            _isDataSelected = false;
        }
    }
}