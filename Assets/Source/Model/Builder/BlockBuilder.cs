using Extensions;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Model.BuilderLogic
{
    public class BlockBuilder
    {
        private readonly Inventory _inventory;
        private readonly Map _map;
        private Block _root;

        public Block Root => _root;

        public event Action<BlockContext> OnPlaced;
        public event Action<Vector2Int> OnRemoved;
        
        private bool IsRootPlacement(Vector2Int position)
            => _root == null && position == _map.RootPosition;
        
        private IEnumerable<BlockSide> GetParentSidesInVicinity(Vector2Int position)
        {
            IEnumerable<Vector2Int> vicinityPositions = _map.GetVicinity(position)
                .Where(p => _map.PositionInMap(p) && _map[p].IsOccupied);

            foreach(Vector2Int vicinityPosition in vicinityPositions)
            {
                BlockSide parentConnectionSide = BlockSideMapper.BlockSideFromParentPosition(vicinityPosition, position);
                if(_map[vicinityPosition].Block.IsAppendCorrect(parentConnectionSide))
                    yield return parentConnectionSide.Reverse();
            }
        }

        private bool ExistOnlyOneParent(Vector2Int position, out BlockSide connectionSide)
        {
            IEnumerable<BlockSide> parentSides = GetParentSidesInVicinity(position);
            if(parentSides.Count() != 1)
            {
                connectionSide = BlockSide.NONE;
                return false;
            }

            connectionSide = parentSides.First();
            return true;
        }

        private bool TryPlaceRoot(IBlockData blockData)
        {
            BlockContext context = BlockContext.CreateRootContext();
            if(_inventory.TryPullOut(blockData, context, out Block root) == false)
                return false;

            _root = root;
            _map[_map.RootPosition].TryPlace(root);
            OnPlaced?.Invoke(context);
            return true;
        }


        public BlockBuilder(Map map, Inventory inventory)
        {
            _map = map;
            _inventory = inventory;

            _root = null;
        }

        private bool CanPlace(Vector2Int position)
            => _map.PositionInMap(position) && _map[position].IsOccupied == false;

        public bool CanAppend(Vector2Int position)
            => CanPlace(position) && (IsRootPlacement(position) || ExistOnlyOneParent(position, out _));

        public bool TryAppend(Vector2Int placementPosition, IBlockData blockData)
        {
            if(CanPlace(placementPosition) == false)
                return false;

            if(IsRootPlacement(placementPosition))
                return TryPlaceRoot(blockData);

            if(ExistOnlyOneParent(placementPosition, out BlockSide connectionSide) == false)
                return false;

            BlockContext context = BlockContext.CreateChildContext(connectionSide);
            if(_inventory.TryPullOut(blockData, context, out Block childBlock) == false)
                return false;

            Vector2Int parentPosition = placementPosition + BlockSideMapper.PositionFromBlockSide(connectionSide);
            Block parent = _map[parentPosition].Block;
            parent.Append(connectionSide.Reverse(), childBlock);
            _map[placementPosition].TryPlace(childBlock);
            OnPlaced?.Invoke(context);

            return true;
        }

        public bool CanRemove(Vector2Int position)
        {
            MapTile tile = _map[position];
            return tile.IsOccupied && tile.Block.HasOperands() == false;
        }

        public bool TryRemove(Vector2Int position)
        {
            if(CanRemove(position) == false)
                return false;
            
            _map[position].RemoveBlock();

            if(position == _map.RootPosition)
                _root = null;
            
            OnRemoved?.Invoke(position);
            return true;
        }
    }
}