using System;
using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class Root : BinaryOperationState
    {
        public Root() : base(BlockSide.ALL) {}

        public override BinaryOperaionStateType StateType => BinaryOperaionStateType.ROOT;

        public override BinaryOperaionStateType NextState(BlockSide operandSide)
        {
            switch(operandSide)
            {
                case BlockSide.UP:
                case BlockSide.DOWN:
                    return BinaryOperaionStateType.OPERANDS_VERTICALLY;
                case BlockSide.LEFT:
                case BlockSide.RIGHT:
                    return BinaryOperaionStateType.OPERANDS_HORIZONTALLY;
                default:
                    return BinaryOperaionStateType.ROOT;
            }
        }
    }
}