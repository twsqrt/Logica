using System;
using UnityEngine;

namespace Model.LogicBlockLogic
{
    public abstract class LogicBlock
    {
        protected readonly LogicBlock _parent;
        protected readonly Vector2Int _position;

        public event Action<LogicBlock> OnRemove;

        public Vector2Int Position => _position;

        public LogicBlock(Vector2Int position, LogicBlock parent)
        {
            _position = position;
            _parent = parent;
        }

        protected void OnRemoveInvoke() => OnRemove?.Invoke(this);

        public abstract bool TryRemove();

        public abstract bool CanAppend(Vector2Int operandPosition);

        public abstract void Append(LogicBlock operand);

        public abstract bool IsCorrectTree();
    }
}