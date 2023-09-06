using UnityEngine;

namespace Model.BlockLogic
{
    public static class BlockSideMapper
    {
        public static BlockSide BlockSideFromOffset(Vector2Int offset) 
            => (offset.x, offset.y) switch
        {
            (1, 0) => BlockSide.RIGHT,
            (-1, 0) => BlockSide.LEFT,
            (0, 1) => BlockSide.UP,
            (0, -1) => BlockSide.DOWN,
            _ => BlockSide.NONE,
        };

        public static BlockSide BlockSideFromParentPosition(Vector2Int childPosition, Vector2Int parentPosition)
            => BlockSideFromOffset(parentPosition - childPosition);

        public static Vector2Int PositionFromBlockSide(BlockSide blockSide)
            => blockSide switch
        {
            BlockSide.RIGHT => Vector2Int.right,
            BlockSide.LEFT => Vector2Int.left,
            BlockSide.UP => Vector2Int.up,
            BlockSide.DOWN => Vector2Int.down,
            _ => Vector2Int.zero,
        };

        public static float AngleFromBlockSide(BlockSide blockSide)
            => blockSide switch
        {
            BlockSide.RIGHT => 0f,
            BlockSide.UP => 90f,
            BlockSide.LEFT => 180f,
            BlockSide.DOWN => 270f,
            _ => 0f,
        };
    }
}