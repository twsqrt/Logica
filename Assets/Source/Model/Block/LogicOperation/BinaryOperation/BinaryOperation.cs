using System.Collections.Generic;
using System.Linq;
using Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class BinaryOperation : LogicOperation
    {
        private readonly List<Block> _operands;
        private readonly Dictionary<BinaryOperationStateType, BinaryOperationState> _states;

        private Stack<BinaryOperationStateType> _stateHistory;
        private BinaryOperationState _currentState;

        public Block FirstOperand => _operands.ElementAtOrDefault(0);
        public Block SecondOperand => _operands.ElementAtOrDefault(1);

        private BinaryOperationStateType GetStartStateType()
        {
            switch(_context.DirectionToParent)
            {
                case Direction.UP:
                case Direction.DOWN:
                    return BinaryOperationStateType.OPERANDS_HORIZONTALLY;
                case Direction.LEFT:
                case Direction.RIGHT:
                    return BinaryOperationStateType.OPERANDS_VERTICALLY;
                default:
                    return BinaryOperationStateType.ROOT;
            }
        }

        private void RemoveChild(Block block)
        {
            _operands.Remove(block);
            _currentState = _states[_stateHistory.Pop()];
            OnSubTreeChangedInvoke();
        }

        public BinaryOperation(LogicOperationType type, BlockContext context) : base(type, context)
        {
            _operands = new List<Block>();

            _states = new Dictionary<BinaryOperationStateType, BinaryOperationState>()
            {
                {BinaryOperationStateType.ROOT, new Root()},
                {BinaryOperationStateType.OPERANDS_HORIZONTALLY, OperandsOnLine.Horizontally(_operands)},
                {BinaryOperationStateType.OPERANDS_VERTICALLY, OperandsOnLine.Vertically(_operands)},
                {BinaryOperationStateType.ALL_OPERANDS_ADDED, new AllOperandsAdded()}
            };

            _stateHistory = new Stack<BinaryOperationStateType>();
            _currentState = _states[GetStartStateType()];
        }

        public override bool IsAppendCorrect(Direction direction)
            => _currentState.IsAppendCorrect(direction);

        public override void Append(Direction direction, Block childBlock)
        {
            _operands.Add(childBlock);
            childBlock.OnSubTreeChanged += OnSubTreeChangedInvoke;
            childBlock.OnDestroy += RemoveChild;

            _stateHistory.Push(_currentState.StateType);

            BinaryOperationStateType nextStateType = _currentState.NextState(direction);
            _currentState = _states[nextStateType];

            OnSubTreeChangedInvoke();
        }

        public override bool HasOperands()
            => _operands.Any();

        public override T Accept<T>(IBlockVisitor<T> visitor)
            => visitor.Visit(this);
    }
}