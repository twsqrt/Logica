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
        private readonly BinaryOperaionStateType _type;

        private OperandsOnLine( 
            IEnumerable<Block> operands, 
            BinaryOperaionStateType type, 
            BlockSide operandCorrectSides) : base(operandCorrectSides)
        {
            _operands = operands;
            _type = type;
        }

        public static OperandsOnLine Horizontally(IEnumerable<Block> operands)
            => new OperandsOnLine(operands, BinaryOperaionStateType.OPERANDS_HORIZONTALLY, BlockSide.HORIZONTALLY);

        public static OperandsOnLine Vertically(IEnumerable<Block> operands)
            => new OperandsOnLine(operands, BinaryOperaionStateType.OPERANDS_VERTICALLY, BlockSide.VERTICALLY);

        public override BinaryOperaionStateType StateType => _type;

        public override BinaryOperaionStateType NextState(BlockSide side)
        {
            if(_operands.Count() > 1)
                return BinaryOperaionStateType.ALL_OPERANDS_ADDED;
            return _type;
        }
    }
}