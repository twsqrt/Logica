using Model.BlockLogic;

namespace Extensions
{
    public static class BlockSideExtension 
    {
        public static BlockSide Reverse(this BlockSide side)
        => side switch
        {
            BlockSide.UP => BlockSide.DOWN,
            BlockSide.DOWN => BlockSide.UP,
            BlockSide.LEFT => BlockSide.RIGHT,
            BlockSide.RIGHT => BlockSide.LEFT,
            _ => BlockSide.NONE,
        };
    }
}