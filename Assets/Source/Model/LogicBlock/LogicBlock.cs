using System;
using UnityEngine;

namespace Model.LogicBlockLogic
{
    public abstract class LogicBlock
    {
        private readonly LogicBlockType _type;
        protected LogicBlock _parent;
        protected Vector2Int _position;

        public event Action<LogicBlock> OnRemove;

        public Vector2Int Position => _position;

        public LogicBlockType BlockType => _type;

        public LogicBlock(LogicBlockType type, Vector2Int position)
        {
            _type = type;
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