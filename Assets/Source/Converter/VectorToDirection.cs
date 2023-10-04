using System;
using Model.BlocksLogic;
using UnityEngine;

namespace Converter
{
    public class VectorToDirection : IConverter<Vector2Int, Direction>
    {
        public Direction Convert(Vector2Int offset)
        => (Math.Sign(offset.x), Math.Sign(offset.y)) switch
        {
            (1, 0) => Direction.RIGHT,
            (-1, 0) => Direction.LEFT,
            (0, 1) => Direction.UP,
            (0, -1) => Direction.DOWN,
            _ => Direction.NONE,
        };
    }
}