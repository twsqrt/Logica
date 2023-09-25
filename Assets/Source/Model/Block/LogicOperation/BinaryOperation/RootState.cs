using System;
using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class RootState : BinaryOperationState
    {
        public RootState() : base(Direction.ALL) {}

        public override BinaryOperationStateType StateType => BinaryOperationStateType.ROOT;

        public override BinaryOperationStateType NextState(Direction direction)
        {
            switch(direction)
            {
                case Direction.UP:
                case Direction.DOWN:
                    return BinaryOperationStateType.OPERANDS_VERTICALLY;
                case Direction.LEFT:
                case Direction.RIGHT:
                    return BinaryOperationStateType.OPERANDS_HORIZONTALLY;
                default:
                    return BinaryOperationStateType.ROOT;
            }
        }
    }
}