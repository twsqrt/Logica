using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class AllOperandsAdded : BinaryOperationState
    {
        public AllOperandsAdded() : base(BlockSide.NONE) {}

        public override BinaryOperaionStateType StateType => BinaryOperaionStateType.ALL_OPERANDS_ADDED;

        public override BinaryOperaionStateType NextState(BlockSide side)
            => BinaryOperaionStateType.ALL_OPERANDS_ADDED;
    }
}