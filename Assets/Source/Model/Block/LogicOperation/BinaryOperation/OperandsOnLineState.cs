using System.Collections.Generic;
using System.Linq; 

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class OperandsOnLineState : BinaryOperationState
    {
        private readonly IEnumerable<Block> _operands;
        private readonly BinaryOperationStateType _type;

        private OperandsOnLineState( 
            IEnumerable<Block> operands, 
            BinaryOperationStateType type, 
            Direction operandCorrectDirections) : base(operandCorrectDirections)
        {
            _operands = operands;
            _type = type;
        }

        public static OperandsOnLineState Horizontally(IEnumerable<Block> operands)
            => new OperandsOnLineState(operands, BinaryOperationStateType.OPERANDS_HORIZONTALLY, Direction.HORIZONTALLY);

        public static OperandsOnLineState Vertically(IEnumerable<Block> operands)
            => new OperandsOnLineState(operands, BinaryOperationStateType.OPERANDS_VERTICALLY, Direction.VERTICALLY);

        public override BinaryOperationStateType StateType => _type;

        public override BinaryOperationStateType NextState(Direction direction)
        {
            if(_operands.Count() > 1)
                return BinaryOperationStateType.ALL_OPERANDS_ADDED;
            return _type;
        }
    }
}