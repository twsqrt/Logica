using Model.BlockLogic;

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
    }
}