using System;
using UnityEngine;

namespace Model.BlockLogic
{
    public abstract class Block
    {
        protected readonly BlockPositionContext _context;

        public event Action<Block> OnRemove;

        public BlockPositionContext Context => _context;

        public Vector2Int Position => _context.Position;

        public Block(BlockPositionContext positionContext)
        {
            _context = positionContext;
        }

        protected void OnRemoveInvoke() => OnRemove?.Invoke(this);

        public abstract bool TryRemove();

        public abstract bool CanAppend(Vector2Int operandPosition);

        public abstract void Append(Block operand);

        public abstract bool IsCorrectTree();

        public abstract T Accept<T>(IBlockVisitor<T> visitor);
    }
}