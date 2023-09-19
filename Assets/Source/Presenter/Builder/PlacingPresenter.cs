using Extensions;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Presenter.BuilderLogic
{
    public class PlacingPresenter : BuilderPresenterState
    {
        private readonly Map _map;
        private readonly Inventory _inventory;

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

        private IEnumerable<Vector2Int> GetParentsInVicinity(Vector2Int position)
        {
            foreach(Vector2Int vicinityPosition in _map.GetVicinity(position).Where(p => _map[p].IsOccupied))
            {
                BlockSide toVicinityCenter = BlockSideMapper.BlockSideFromParentPosition(vicinityPosition, position);
                if(_map[vicinityPosition].Block.IsAppendCorrect(toVicinityCenter))
                    yield return vicinityPosition;
            }
        }

        private bool ExistOnlyOneParent(Vector2Int position, out Vector2Int parentPosition)
        {
            IEnumerable<Vector2Int> parentPositions = GetParentsInVicinity(position);
            parentPosition = parentPositions.SingleOrDefault();
            return parentPositions.Count() == 1;
        }

        private bool TryExecuteForRoot(IBlockData _currentData)
        {
            BlockContext context = BlockContext.CreateRootContext();
            if(_inventory.TryPullOut(_currentData, context, out Block root) == false)
                return false;

            _map[_map.RootPosition].TryPlace(root);
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
            => positions.Where(p => CanPlace(p) && (p == _map.RootPosition || ExistOnlyOneParent(p, out _)));

        public override bool TryExecute(Vector2Int position)
        {
            if(_isDataSelected == false || CanPlace(position) == false)
                return false;

            if(position == _map.RootPosition)
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