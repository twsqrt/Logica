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
            => _root == null && position == _map.ExecutionPosition;
        
        private IEnumerable<Block> GetParentsInVicinity(Vector2Int position)
            => _map
                .GetVicinityInMap(position)
                .Where(p => _map.PositionInMap(p) && _map[p].IsOccupied)
                .Select(p => _map[p].Block)
                .Where(b => b.CanAppend(position));

        private bool ExistOnlyOneParent(Vector2Int position, out Block parent)
        {
            IEnumerable<Block> parentsInVicinity = GetParentsInVicinity(position);
            if(parentsInVicinity.Count() != 1)
            {
                parent = null;
                return false;
            }

            parent = parentsInVicinity.Single();
            return true;
        }

        private bool TryPlaceRoot(IBlockData blockData)
        {
            BlockContext context = BlockContext.CreateRootContext(_map.ExecutionPosition);
            if(_inventory.TryPullOut(blockData, context, out Block root) == false)
                return false;

            _root = root;
            _map[context.Position].TryPlace(root);
            OnPlaced?.Invoke(context);
            return true;
        }


        public BlockBuilder(Map map, Inventory inventory)
        {
            _map = map;
            _inventory = inventory;

            _root = null;
        }
        
        public bool CanPlace(Vector2Int position)
            => _map.CanPlace(position) && (IsRootPlacement(position) || ExistOnlyOneParent(position, out _));

        public bool TryPlace(Vector2Int placementPosition, IBlockData blockData)
        {
            if(_map.CanPlace(placementPosition) == false)
                return false;

            if(IsRootPlacement(placementPosition))
                return TryPlaceRoot(blockData);

            if(ExistOnlyOneParent(placementPosition, out Block parent) == false)
                return false;

            BlockSide parentConnectionSide = BlockSideMapper.BlockSideFromParentPosition(placementPosition, parent.Position);
            BlockContext context = BlockContext.CreateChildContext(parentConnectionSide, placementPosition);
            if(_inventory.TryPullOut(blockData, context, out Block block) == false)
                return false;

            parent.Append(block);
            _map[context.Position].TryPlace(block);
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

            if(position == _map.ExecutionPosition)
                _root = null;
            
            OnRemoved?.Invoke(position);
            return true;
        }
    }
}