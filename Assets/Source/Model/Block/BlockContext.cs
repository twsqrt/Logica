namespace Model.BlocksLogic
{
    public readonly struct BlockContext
    {
        public readonly bool HasParent;
        public readonly Direction DirectionToParent;

        private BlockContext(bool hasParent, Direction directionToParent)
        {
            HasParent = hasParent;
            DirectionToParent = directionToParent;
        }

        public static BlockContext CreateRootContext()
            => new BlockContext(false, Direction.NONE);
        
        public static BlockContext CreateChildContext(Direction directionToParent)
            => new BlockContext(true, directionToParent);
    }
}