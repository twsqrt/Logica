using UnityEngine;
using Model.BlocksLogic;
using Converter;

namespace Extensions
{
    public static class DirectionExtension 
    {
        public static Direction Reverse(this Direction direction)
        => direction switch
        {
            Direction.UP => Direction.DOWN,
            Direction.DOWN => Direction.UP,
            Direction.LEFT => Direction.RIGHT,
            Direction.RIGHT => Direction.LEFT,
            _ => Direction.NONE,
        };

        public static Vector2Int ToVector(this Direction direction)
        => direction switch
        {
            Direction.RIGHT => Vector2Int.right,
            Direction.LEFT => Vector2Int.left,
            Direction.UP => Vector2Int.up,
            Direction.DOWN => Vector2Int.down,
            _ => Vector2Int.zero,
        };

        public static float Angle(this Direction direction)
        => direction switch
        {
            Direction.RIGHT => 0f,
            Direction.UP => 90f,
            Direction.LEFT => 180f,
            Direction.DOWN => 270f,
            _ => 0f,
        };
    }
}