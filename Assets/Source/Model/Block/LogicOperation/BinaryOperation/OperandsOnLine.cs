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
            BlockSide operandCorrectSides) : base(operandCorrectSides)
        {
            _operands = operands;
            _type = type;
        }

        public static OperandsOnLine Horizontally(IEnumerable<Block> operands)
            => new OperandsOnLine(operands, BinaryOperationStateType.OPERANDS_HORIZONTALLY, BlockSide.HORIZONTALLY);

        public static OperandsOnLine Vertically(IEnumerable<Block> operands)
            => new OperandsOnLine(operands, BinaryOperationStateType.OPERANDS_VERTICALLY, BlockSide.VERTICALLY);

        public override BinaryOperationStateType StateType => _type;

        public override BinaryOperationStateType NextState(BlockSide side)
        {
            if(_operands.Count() > 1)
                return BinaryOperationStateType.ALL_OPERANDS_ADDED;
            return _type;
        }
    }
}