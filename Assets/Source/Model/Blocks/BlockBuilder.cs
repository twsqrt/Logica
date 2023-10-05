using Extensions;
using Model.BlocksLogic.BlocksData;
using Model.BlocksLogic;
using Model.MapLogic;
using System.Linq;
using System;
using UnityEngine;

namespace Model.BlockLogic
{
    public class BlockBuilder
    {
        private readonly BlockFactory _factory;
        private readonly Map _map;

        public BlockBuilder(BlockFactory factory, Map map)
        {
            _factory = factory;
            _map = map;
        }

        private bool TryGetParent(Vector2Int at, out Direction fromCenterToParent)
        {
            MapVicinity vicinity = _map.GetVicinity(at);
            foreach(var (fromCenter, position) in vicinity.Positions.Where(p => _map[p.Value].IsOccupied))
            {
                Block block = _map[position].Block;
                Direction toCenter = fromCenter.Reverse();
                if(block.IsAppendCorrect(toCenter))
                {
                    fromCenterToParent = fromCenter;
                    return true;
                }
            }

            fromCenterToParent = Direction.NONE;
            return false;
        }

        public bool CanPlace(Vector2Int position)
            => _map[position].IsOccupied == false;

        public Block Place(IBlockData data, Vector2Int position)
        {
            if(_map[position].IsOccupied)
                throw new ArgumentException("Can't create block on occupied tile");
            
            Block block;
            if(TryGetParent(position, out Direction fromCenterToParent))
            {
                BlockContext context = BlockContext.CreateChildContext(fromCenterToParent);
                block = _factory.Create(data, context);
                Block parent = _map[position + fromCenterToParent.ToVector()].Block;
                parent.Append(fromCenterToParent.Reverse(), block);
            }
            else
            {
                BlockContext context = BlockContext.CreateRootContext();
                block = _factory.Create(data, context);
            }

            _map[position].PlaceBlock(block);
            return block;
        }
    }
}