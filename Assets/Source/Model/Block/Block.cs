using System;
using UnityEngine;

namespace Model.BlockLogic
{
    public abstract class Block
    {
        protected readonly Block _parent;
        protected readonly Vector2Int _position;

        public event Action<Block> OnRemove;

        public Vector2Int Position => _position;

        public Block(Vector2Int position, Block parent)
        {
            _position = position;
            _parent = parent;
        }

        protected void OnRemoveInvoke() => OnRemove?.Invoke(this);

        public abstract bool TryRemove();

        public abstract bool CanAppend(Vector2Int operandPosition);

        public abstract void Append(Block operand);

        public abstract bool IsCorrectTree();
    }
}