using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Model.BlockLogic
{
    public static class ParentPositionMapper
    {
        public static ParentBlockPosition ParentPositionFrom(Vector2Int offset) 
            => (offset.x, offset.y) switch
        {
            (1, 0) => ParentBlockPosition.RIGHT,
            (-1, 0) => ParentBlockPosition.LEFT,
            (0, 1) => ParentBlockPosition.UP,
            (0, -1) => ParentBlockPosition.DOWN,
            _ => ParentBlockPosition.NONE,
        };

        public static ParentBlockPosition ParentPositionFrom(Vector2Int childPosition, Vector2Int parentPosition)
            => ParentPositionFrom(parentPosition - childPosition);
    }
}