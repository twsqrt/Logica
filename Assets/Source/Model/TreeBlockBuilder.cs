using Model.MapLogic;
using Model.LogicBlockLogic;
using System.Linq;
using UnityEngine;
using Model.LogicBlockLogic.BinaryOperationLogic;
using System.Collections.Generic;

namespace Model
{
    public class TreeBlockBuilder
    {
        private readonly Map _map;
        private LogicBlock _root;

        public TreeBlockBuilder(Map map)
        {
            _map = map;
            _root = null;
        }

        private void PlaceRoot(LogicBlock newRoot)
        {
            _root = newRoot;
            _map[_map.ExecutionPosition].Block = newRoot;
        }
        
        private bool IsRootPlacement(Vector2Int position)
            => _root == null && position == _map.ExecutionPosition;
        
        private IEnumerable<LogicBlock> GetParentsInVicinity(Vector2Int position)
            => _map
                .GetVicinityInMap(position)
                .Where(p => _map.PositionInMap(p) && _map[p].IsOccupied)
                .Select(p => _map[p].Block)
                .Where(b => b.CanAppend(position));

        private bool ExistOnlyOneParent(Vector2Int position, out LogicBlock parent)
        {
            IEnumerable<LogicBlock> parentsInVicinity = GetParentsInVicinity(position);
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

        public bool TryPlace(Vector2Int position, LogicBlockType blockType)
        {
            if(_map.CanPlace(position) == false)
                return false;

            if(IsRootPlacement(position))
            {
                //temp
                PlaceRoot(new BinaryOperaion(blockType, position, null));
                return true;
            }

            if(ExistOnlyOneParent(position, out LogicBlock parent) == false)
                return false;

            //temp
            LogicBlock block = new BinaryOperaion(blockType, position, parent);
            parent.Append(block);
            _map[position].Block = block;

            return true;
        }

        public bool TryRemove(Vector2Int position)
        {
            MapTile tile = _map[position];
            if(tile.IsOccupied == false)
                return false;
            
            LogicBlock block = tile.Block;
            return block.TryRemove();
        }
    }
}