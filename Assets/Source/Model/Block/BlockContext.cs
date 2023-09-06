using UnityEngine;

namespace Model.BlockLogic
{
    public readonly struct BlockContext
    {
        public readonly bool HasParent;
        public readonly BlockSide ParentConnectionSide;
        public readonly Vector2Int Position;

        private BlockContext(bool hasParent, BlockSide parentConnectionSide, Vector2Int position)
        {
            HasParent = hasParent;
            ParentConnectionSide = parentConnectionSide;
            Position = position;
        }

        public static BlockContext CreateRootContext(Vector2Int position)
            => new BlockContext(false, BlockSide.NONE, position);
        
        public static BlockContext CreateChildContext(BlockSide parentConnectionSide, Vector2Int position)
            => new BlockContext(true, parentConnectionSide, position);
    }
}