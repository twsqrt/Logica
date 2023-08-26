using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Model.BlockLogic
{
    public static class BlockSideMapper
    {
        public static BlockSide From(Vector2Int offset) 
            => (offset.x, offset.y) switch
        {
            (1, 0) => BlockSide.RIGHT,
            (-1, 0) => BlockSide.LEFT,
            (0, 1) => BlockSide.UP,
            (0, -1) => BlockSide.DOWN,
            _ => BlockSide.UNDEFINED,
        };

        public static BlockSide From(Vector2Int childPosition, Vector2Int parentPosition)
            => From(parentPosition - childPosition);
    }
}