using System;
using System.Collections.Generic;
using System.Linq; 
using Model.MapLogic;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class OperandsOnLine : BinaryOperationState
    {
        private readonly IEnumerable<Block> _operands;
        private readonly BinaryOperationStateType _type;

        private OperandsOnLine( 
            IEnumerable<Block> operands, 
            BinaryOperationStateType type, 
            Direction operandCorrectDirections) : base(operandCorrectDirections)
        {
            _operands = operands;
            _type = type;
        }

        public static OperandsOnLine Horizontally(IEnumerable<Block> operands)
            => new OperandsOnLine(operands, BinaryOperationStateType.OPERANDS_HORIZONTALLY, Direction.HORIZONTALLY);

        public static OperandsOnLine Vertically(IEnumerable<Block> operands)
            => new OperandsOnLine(operands, BinaryOperationStateType.OPERANDS_VERTICALLY, Direction.VERTICALLY);

        public override BinaryOperationStateType StateType => _type;

        public override BinaryOperationStateType NextState(Direction direction)
        {
            if(_operands.Count() > 1)
                return BinaryOperationStateType.ALL_OPERANDS_ADDED;
            return _type;
        }
    }
}