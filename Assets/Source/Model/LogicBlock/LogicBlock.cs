using System;
using UnityEngine;

namespace Model.LogicBlockLogic
{
    public abstract class LogicBlock
    {
        protected LogicBlock _parent;
        protected Vector2Int _position;

        public event Action<LogicBlock> OnRemove;

        public Vector2Int Position => _position;

        public LogicBlock(Vector2Int position)
        {
            _position = position;
            _parent = null;
        }

        public virtual void SetParent(LogicBlock parent)
        {
            _parent = parent;
        }

        protected void OnRemoveInvoke() => OnRemove?.Invoke(this);

        public abstract bool TryRemove();

        public abstract bool CanAppend(Vector2Int operandPosition);

        public abstract void Append(LogicBlock operand);

        public abstract bool IsCorrectTree();
    }
}