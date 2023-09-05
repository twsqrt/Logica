using Model.MapLogic;
using Model.BlockLogic;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Model.BlockLogic.BlockDataLogic;
using Model.InventoryLogic;

namespace Model.BuilderLogic
{
    public class BlockBuilder
    {
        private readonly Inventory _inventory;
        private readonly Map _map;
        private Block _root;

        public BlockBuilder(Map map, Inventory inventory)
        {
            _map = map;
            _inventory = inventory;

            _root = null;
        }
        
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
        
        public bool CanPlace(Vector2Int position)
        {
            return _map.CanPlace(position) && (IsRootPlacement(position) || ExistOnlyOneParent(position, out _));
        }

        private bool TryPlaceRoot(IBlockData blockData)
        {
            var context = new BlockPositionContext{Position = _map.ExecutionPosition};
            if(_inventory.TryPullOut(blockData, context, out Block root) == false)
                return false;

            _root = root;
            _map[context.Position].TryPlaceBlock(root);
            return true;
        }

        public bool TryPlace(Vector2Int blockPosition, IBlockData blockData)
        {
            if(_map.CanPlace(blockPosition) == false)
                return false;

            if(IsRootPlacement(blockPosition))
                return TryPlaceRoot(blockData);

            if(ExistOnlyOneParent(blockPosition, out Block parent) == false)
                return false;

            var context = new BlockPositionContext{
                ConnectionSide = BlockSideMapper.BlockSideFromParentPosition(blockPosition, parent.Position),
                Position = blockPosition,
            };

            if(_inventory.TryPullOut(blockData, context, out Block block) == false)
                return false;

            parent.Append(block);
            _map[context.Position].TryPlaceBlock(block);
            return true;
        }

        public bool TryRemove(Vector2Int position)
        {
            MapTile tile = _map[position];
            if(tile.IsOccupied == false || tile.TryRemoveBlock() == false)
                return false;

            if(position == _map.ExecutionPosition)
                _root = null;
            
            return true;
        }
    }
}