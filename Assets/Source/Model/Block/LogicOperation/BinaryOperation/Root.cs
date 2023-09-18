using System;
using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class Root : BinaryOperationState
    {
        public Root() : base(BlockSide.ALL) {}

        public override BinaryOperationStateType StateType => BinaryOperationStateType.ROOT;

        public override BinaryOperationStateType NextState(BlockSide operandSide)
        {
            switch(operandSide)
            {
                case BlockSide.UP:
                case BlockSide.DOWN:
                    return BinaryOperationStateType.OPERANDS_VERTICALLY;
                case BlockSide.LEFT:
                case BlockSide.RIGHT:
                    return BinaryOperationStateType.OPERANDS_HORIZONTALLY;
                default:
                    return BinaryOperationStateType.ROOT;
            }
        }
    }
}