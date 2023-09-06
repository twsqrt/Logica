using System;
using UnityEngine;

namespace Model.BlockLogic
{
    public abstract class Block
    {
        protected readonly BlockContext _context;

        public event Action<Block> OnRemove;

        public BlockContext Context => _context;

        public Vector2Int Position => _context.Position;

        public Block(BlockContext positionContext)
        {
            _context = positionContext;
        }

        public abstract bool CanBeRemoved();

        public bool TryRemove()
        {
            if(CanBeRemoved())
            {
                OnRemove?.Invoke(this);
                return true;
            }

            return false;
        }

        public abstract bool CanAppend(Vector2Int operandPosition);

        public abstract void Append(Block operand);

        public abstract bool IsCorrectTree();

        public abstract T Accept<T>(IBlockVisitor<T> visitor);
    }
}