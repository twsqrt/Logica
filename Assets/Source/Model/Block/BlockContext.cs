using UnityEngine;

namespace Model.BlockLogic
{
    public readonly struct BlockContext
    {
        public readonly bool HasParent;
        public readonly BlockSide ParentConnectionSide;

        private BlockContext(bool hasParent, BlockSide parentConnectionSide)
        {
            HasParent = hasParent;
            ParentConnectionSide = parentConnectionSide;
        }

        public static BlockContext CreateRootContext()
            => new BlockContext(false, BlockSide.NONE);
        
        public static BlockContext CreateChildContext(BlockSide parentConnectionSide)
            => new BlockContext(true, parentConnectionSide);
    }
}