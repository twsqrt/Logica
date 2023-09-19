using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class AllOperandsAdded : BinaryOperationState
    {
        public AllOperandsAdded() : base(Direction.NONE) {}

        public override BinaryOperationStateType StateType => BinaryOperationStateType.ALL_OPERANDS_ADDED;

        public override BinaryOperationStateType NextState(Direction direction)
            => BinaryOperationStateType.ALL_OPERANDS_ADDED;
    }
}