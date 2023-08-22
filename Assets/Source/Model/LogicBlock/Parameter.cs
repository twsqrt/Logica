using System;
using UnityEngine;

namespace Model.LogicBlockLogic
{
    public class Parameter : LogicBlock
    {
        public Parameter(Vector2Int position, LogicBlock parent) : base(LogicBlockType.PARAMETER, position, parent) {}

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