using Model.MapLogic;
using Model.BlockLogic;
using System.Linq;
using UnityEngine;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using System.Collections.Generic;

namespace Model
{
    public class TreeBlockBuilder
    {
        private readonly Map _map;
        private Block _root;

        public TreeBlockBuilder(Map map)
        {
            _map = map;
            _root = null;
        }

        private void PlaceRoot(Block newRoot)
        {
            _root = newRoot;
            _map[_map.ExecutionPosition].Block = newRoot;
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

        public bool TryPlace(Vector2Int position, LogicOperationType blockType)
        {
            if(_map.CanPlace(position) == false)
                return false;

            if(IsRootPlacement(position))
            {
                //temp
                PlaceRoot(new BinaryOperaion(blockType, position, null));
                return true;
            }

            if(ExistOnlyOneParent(position, out Block parent) == false)
                return false;

            //temp
            Block block = new BinaryOperaion(blockType, position, parent);
            parent.Append(block);
            _map[position].Block = block;

            return true;
        }

        public bool TryRemove(Vector2Int position)
        {
            MapTile tile = _map[position];
            if(tile.IsOccupied == false)
                return false;
            
            Block block = tile.Block;
            return block.TryRemove();
        }
    }
}