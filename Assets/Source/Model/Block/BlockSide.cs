using System;

namespace Model.BlockLogic
{
    [Flags]
    public enum Direction
    {
        NONE = 0,
        UP = 1,
        DOWN = 2,
        LEFT = 4,
        RIGHT = 8,

        VERTICALLY = 3,
        HORIZONTALLY = 12,
        ALL = 15,
    }
}