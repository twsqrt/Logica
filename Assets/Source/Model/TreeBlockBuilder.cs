using Model.MapLogic;
using Model.LogicBlockLogic;
using System.Linq;
using UnityEngine;
using System.Net.Http.Headers;

namespace Model
{
    public class TreeBlockBuilder
    {
        private readonly Map _map;
        private readonly LogicBlock _root;

        public TreeBlockBuilder(Map map)
        {
            _map = map;
            _root = null;
        }

        public bool PlaceRule(Vector2Int position)
            => _map.CanPlace(position) && Map.GetVicinity(position).Where(p => _map[p].IsOccupied).Count() == 1;

        public bool TryPlace(Vector2Int position, LogicBlock newBlock)
        {
            if(PlaceRule(position) == false)
                return false;

            MapTile parentMapTile = Map.GetVicinity(position).Select(p => _map[p]).Single(t => t.IsOccupied);
            LogicBlock parentBlock = parentMapTile.Block;

            if(parentBlock.CanAppend(position))
            {
                parentBlock.Append(newBlock);
                newBlock.SetParent(parentBlock);
                return true;
            }

            return false;
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