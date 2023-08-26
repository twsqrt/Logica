using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.LogicBlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class BinaryOperaion : LogicOperation
    {
        private readonly List<LogicBlock> _operands;
        private readonly Dictionary<BinaryOperaionStateType, IBinaryOperationState> _states;

        private Stack<BinaryOperaionStateType> _stateHistory;
        private IBinaryOperationState _currentState;

        public BinaryOperaion(LogicOperationType type, Vector2Int position, LogicBlock parent) : base(type, position, parent)
        {
            _operands = new List<LogicBlock>();

            _states = new Dictionary<BinaryOperaionStateType, IBinaryOperationState>()
            {
                {BinaryOperaionStateType.ROOT, new Root(_position)},
                {BinaryOperaionStateType.OPERANDS_HORIZONTALLY, OperandsOnLine.Horizontally(_position, _operands)},
                {BinaryOperaionStateType.OPERANDS_VERTICALLY, OperandsOnLine.Vertically(_position, _operands)},
                {BinaryOperaionStateType.ALL_OPERANDS_ADDED, new AllOperandsAdded()}
            };

            _stateHistory = new Stack<BinaryOperaionStateType>();
            _currentState = _states[GetStartStateType()];
        }

        private BinaryOperaionStateType GetStartStateType()
        {
            if(_parent == null)
                return BinaryOperaionStateType.ROOT;
            
            if(_parent.Position.x == _position.x)
                return BinaryOperaionStateType.OPERANDS_HORIZONTALLY;
            return BinaryOperaionStateType.OPERANDS_VERTICALLY; 
        }

        private void OnRemoveHandler(LogicBlock block)
        {
            _operands.Remove(block);
            _currentState = _states[_stateHistory.Pop()];
        }

        public override bool CanAppend(Vector2Int operandPosition)
            => _currentState.CanAppend(operandPosition);

        public override void Append(LogicBlock operand)
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