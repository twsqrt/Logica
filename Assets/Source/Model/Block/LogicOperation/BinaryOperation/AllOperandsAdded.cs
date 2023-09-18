using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class AllOperandsAdded : BinaryOperationState
    {
        public AllOperandsAdded() : base(BlockSide.NONE) {}

        public override BinaryOperationStateType StateType => BinaryOperationStateType.ALL_OPERANDS_ADDED;

        public override BinaryOperationStateType NextState(BlockSide side)
            => BinaryOperationStateType.ALL_OPERANDS_ADDED;
    }
}