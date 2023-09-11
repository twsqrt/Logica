using System.Collections.Generic;
using System.Linq;
using Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class BinaryOperaion : LogicOperation
    {
        private readonly List<Block> _operands;
        private readonly Dictionary<BinaryOperaionStateType, BinaryOperationState> _states;

        private Stack<BinaryOperaionStateType> _stateHistory;
        private BinaryOperationState _currentState;

        public Block FirstOperand => _operands.ElementAtOrDefault(0);
        public Block SecondOperand => _operands.ElementAtOrDefault(1);

        private BinaryOperaionStateType GetStartStateType()
        {
            switch(_context.ParentConnectionSide)
            {
                case BlockSide.UP:
                case BlockSide.DOWN:
                    return BinaryOperaionStateType.OPERANDS_HORIZONTALLY;
                case BlockSide.LEFT:
                case BlockSide.RIGHT:
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

        public BinaryOperaion(LogicOperationType type, BlockContext context) : base(type, context)
        {
            _operands = new List<Block>();

            _states = new Dictionary<BinaryOperaionStateType, BinaryOperationState>()
            {
                {BinaryOperaionStateType.ROOT, new Root()},
                {BinaryOperaionStateType.OPERANDS_HORIZONTALLY, OperandsOnLine.Horizontally(_operands)},
                {BinaryOperaionStateType.OPERANDS_VERTICALLY, OperandsOnLine.Vertically(_operands)},
                {BinaryOperaionStateType.ALL_OPERANDS_ADDED, new AllOperandsAdded()}
            };

            _stateHistory = new Stack<BinaryOperaionStateType>();
            _currentState = _states[GetStartStateType()];
        }

        public override bool CanAppend(BlockSide side)
            => _currentState.CanAppend(side);

        public override void Append(Block operand)
        {
            _operands.Add(operand);
            operand.OnDestroy += OnRemoveHandler;

            _stateHistory.Push(_currentState.StateType);

            BlockSide operandSide = BlockSideMapper.BlockSideFromParentPosition(Position, operand.Position);
            BinaryOperaionStateType nextStateType = _currentState.NextState(operandSide);
            _currentState = _states[nextStateType];
        }

        public override bool IsCorrectTree()
        {
            if(_operands.Count() != 2)
                return false;
            return FirstOperand.IsCorrectTree() && SecondOperand.IsCorrectTree();
        }

        public override bool HasOperands()
            => _operands.Any();

        public override T Accept<T>(IBlockVisitor<T> visitor)
            => visitor.Visit(this);
    }
}