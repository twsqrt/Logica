using System;
using UnityEngine;

namespace Model.LogicBlockLogic
{
    public class Parameter : LogicBlock
    {
        private readonly int _id;

        public int Id => _id;

        public Parameter(int id, Vector2Int position, LogicBlock parent) : base(position, parent)
        {
            _id = id;
        }

        public override bool CanAppend(Vector2Int operandPosition) => false;

        public override void Append(LogicBlock operand)
        {
            throw new InvalidOperationException();
        }

        public override bool IsCorrectTree() => true;

        public override bool TryRemove()
        {
            OnRemoveInvoke();
            return true;
        }
    }
}