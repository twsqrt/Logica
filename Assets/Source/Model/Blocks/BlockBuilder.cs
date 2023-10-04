using Converter;
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
        private readonly IConverter<Vector2Int, Direction> _vectorToDirection;

        public BlockBuilder(BlockFactory factory, Map map, IConverter<Vector2Int, Direction> vectorToDirection)
        {
            _factory = factory;
            _map = map;
            _vectorToDirection = vectorToDirection;
        }

        private bool TryGetParent(Vector2Int position, out Direction toParent)
        {
            foreach(Vector2Int vicinityPosition in _map.GetVicinity(position).Where(p => _map[p].IsOccupied))
            {
                Block block = _map[vicinityPosition].Block;
                Direction toChild = _vectorToDirection.Convert(position - vicinityPosition);
                if(block.IsAppendCorrect(toChild))
                {
                    toParent = toChild.Reverse();
                    return true;
                }
            }

            toParent = Direction.NONE;
            return false;
        }

        public bool CanPlace(Vector2Int position)
            => _map[position].IsOccupied == false;

        public Block Place(IBlockData data, Vector2Int position)
        {
            if(_map[position].IsOccupied)
                throw new ArgumentException("Can't create block on occupied tile");
            
            Block block;
            if(TryGetParent(position, out Direction toParent))
            {
                BlockContext context = BlockContext.CreateChildContext(toParent);
                block = _factory.Create(data, context);
                Block parent = _map[position + toParent.ToVector()].Block;
                parent.Append(toParent.Reverse(), block);
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