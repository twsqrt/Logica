using System;
using System.Collections.Generic;
using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.LogicBlockLogic
{
    public class BinaryOperaion : LogicBlock
    {
        private readonly List<LogicBlock> _operands;

        private Func<LogicBlock, bool> _currentState;
        private readonly Stack<Func<LogicBlock, bool>> _stateHistory;

        public BinaryOperaion(Vector2Int position) : base(position)
        {
            _operands = new List<LogicBlock>();

            _currentState = Root;
            _stateHistory = new Stack<Func<LogicBlock, bool>>();
        }

        private void OnRemoveHandler(LogicBlock operand)
        {
            _operands.Remove(operand);
            _currentState = _stateHistory.Pop();
        }
        
        private void Append(LogicBlock operand)
        {
            _operands.Add(operand);
            operand.OnRemove += OnRemoveHandler;
        }

        private bool Root(LogicBlock operand)
        {
            Vector2Int operandPosition = operand.Position;
            if(Map.GetVicinity(_position).Contains(operandPosition) == false)
                return false;

            Append(operand);
            
            if(_position.x == operandPosition.x)
                _currentState = OperandsVertically;
            _currentState = OperandsHorizontally;

            return true;
        }

        private bool OperandsByRule(LogicBlock operand, Func<Vector2Int, bool> rule)
        {
            if(Map.GetVicinity(_position).Where(rule).Contains(operand.Position))
            {
                Append(operand);

                if(_operands.Any())
                    _currentState = AllOperandsAdded;

                return true;
            }
            return false;
        }

        private bool OperandsHorizontally(LogicBlock operand)
            => OperandsByRule(operand, p => p.y == _position.y);

        private bool OperandsVertically(LogicBlock operand)
            => OperandsByRule(operand, p => p.x == _position.x);
        
        private bool AllOperandsAdded(LogicBlock operand) => false;

        public override void SetParent(LogicBlock parent)
        {
            _parent = parent;

            if(parent == null)
                _currentState = Root;
            else if(parent.Position.x == _position.x)
                _currentState = OperandsVertically;
            else
                _currentState = OperandsVertically;
        }

        public override bool IsCorrectTree()
        {
            if(_currentState != AllOperandsAdded)
                return false;

            foreach(LogicBlock operand in _operands)
            {
                if(operand.IsCorrectTree() == false)
                    return false;
            }

            return true;
        }

        public override bool TryAppend(LogicBlock operand)
        {
            Func<LogicBlock, bool> previouseState = _currentState;
            if(_currentState(operand))
            {
                _stateHistory.Push(previouseState);
                return true;
            }

            return false;
        }

        public override bool TryRemove()
        {
            if(_operands.Any())
                return false;
            
            OnRemoveInvoke();
            return true;
        }
    }
}