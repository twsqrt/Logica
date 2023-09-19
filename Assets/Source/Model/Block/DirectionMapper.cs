using Unity.VisualScripting;
using UnityEngine;

namespace Model.BlockLogic
{
    public static class DirectionMapper
    {
        public static Direction DirectionFromOffset(Vector2Int offset) 
            => (offset.x, offset.y) switch
        {
            (1, 0) => Direction.RIGHT,
            (-1, 0) => Direction.LEFT,
            (0, 1) => Direction.UP,
            (0, -1) => Direction.DOWN,
            _ => Direction.NONE,
        };

        public static Direction DirectionFromSegment(Vector2Int start, Vector2Int end)
            => DirectionFromOffset(end - start);

        public static Vector2Int OffsetFromDirection(Direction Direction)
            => Direction switch
        {
            Direction.RIGHT => Vector2Int.right,
            Direction.LEFT => Vector2Int.left,
            Direction.UP => Vector2Int.up,
            Direction.DOWN => Vector2Int.down,
            _ => Vector2Int.zero,
        };

        public static float AngleFromDirection(Direction Direction)
            => Direction switch
        {
            Direction.RIGHT => 0f,
            Direction.UP => 90f,
            Direction.LEFT => 180f,
            Direction.DOWN => 270f,
            _ => 0f,
        };
    }
}