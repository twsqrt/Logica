using System;
using UnityEngine;

namespace Model.BlockLogic
{
    public abstract class Block
    {
        protected readonly BlockPositionContext _context;

        public event Action<Block> OnRemove;

        public Vector2Int Position => _context.Position;

        public Block(BlockPositionContext context)
        {
            _context = context;
        }

        protected void OnRemoveInvoke() => OnRemove?.Invoke(this);

        public abstract bool TryRemove();

        public abstract bool CanAppend(Vector2Int operandPosition);

        public abstract void Append(Block operand);

        public abstract bool IsCorrectTree();
    }
}