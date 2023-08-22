using UnityEngine;

namespace Model.LogicBlockLogic
{
    public class Parameter : LogicBlock
    {
        public Parameter(Vector2Int position) : base(position) {}

        public override bool TryAppend(LogicBlock logicOperator) => false;

        public override bool IsCorrectTree() => true;

        public override bool TryRemove()
        {
            OnRemoveInvoke();
            return true;
        }
    }
}