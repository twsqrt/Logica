using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class BinaryOperaion : LogicOperation
    {
        private readonly List<Block> _operands;
        private readonly Dictionary<BinaryOperaionStateType, IBinaryOperationState> _states;

        private Stack<BinaryOperaionStateType> _stateHistory;
        private IBinaryOperationState _currentState;

        public BinaryOperaion(LogicOperationType type, BlockPositionContext context) : base(type, context)
        {
            _operands = new List<Block>();

            _states = new Dictionary<BinaryOperaionStateType, IBinaryOperationState>()
            {
                {BinaryOperaionStateType.ROOT, new Root(_context.Position)},
                {BinaryOperaionStateType.OPERANDS_HORIZONTALLY, OperandsOnLine.Horizontally(_context.Position, _operands)},
                {BinaryOperaionStateType.OPERANDS_VERTICALLY, OperandsOnLine.Vertically(_context.Position, _operands)},
                {BinaryOperaionStateType.ALL_OPERANDS_ADDED, new AllOperandsAdded()}
            };

            _stateHistory = new Stack<BinaryOperaionStateType>();
            _currentState = _states[GetStartStateType()];
        }

        private BinaryOperaionStateType GetStartStateType()
        {
            switch(_context.ParentPosition)
            {
                case ParentBlockPosition.UP:
                case ParentBlockPosition.DOWN:
                    return BinaryOperaionStateType.OPERANDS_HORIZONTALLY;
                case ParentBlockPosition.LEFT:
                case ParentBlockPosition.RIGHT:
                    return BinaryOperaionStateType.OPERANDS_VERTICALLY;
                default:
                    return BinaryOperaionStateType.ROOT;
            }
        }

        private void OnRemoveHandler(Block block)
        {
            _operands.Remove(block);
            _currentState = _states[_stateHistory.Pop()];
        }

        public override bool CanAppend(Vector2Int operandPosition)
            => _currentState.CanAppend(operandPosition);

        public override void Append(Block operand)
        {
            _operands.Add(operand);
            operand.OnRemove += OnRemoveHandler;

            _stateHistory.Push(_currentState.StateType);
            BinaryOperaionStateType nextStateType = _currentState.NextState(operand.Position);
            _currentState = _states[nextStateType];
        }

        public override bool IsCorrectTree()
        {
            if(_operands.Count() != 2)
                return false;
            return _operands[0].IsCorrectTree() && _operands[1].IsCorrectTree();
        }

        public override bool TryRemove()
        {
            return false;
        }
    }
}