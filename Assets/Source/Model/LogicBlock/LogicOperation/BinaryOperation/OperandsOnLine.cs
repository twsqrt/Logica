using System;
using System.Collections.Generic;
using System.Linq; 
using Model.MapLogic;
using UnityEngine;

namespace Model.LogicBlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class OperandsOnLine : IBinaryOperationState
    {
        private readonly Vector2Int _position;
        private readonly IEnumerable<LogicBlock> _operands;
        private readonly BinaryOperaionStateType _type;
        private readonly Func<Vector2Int, Vector2Int, bool> _rule; 

        private OperandsOnLine(Vector2Int position, 
            IEnumerable<LogicBlock> operands, 
            BinaryOperaionStateType type, 
            Func<Vector2Int, Vector2Int, bool> rule)
        {
            _position = position;
            _operands = operands;
            _type = type;
            _rule = rule;
        }

        public static IBinaryOperationState Horizontally(Vector2Int position, IEnumerable<LogicBlock> operands)
            => new OperandsOnLine(position, operands, BinaryOperaionStateType.OPERANDS_HORIZONTALLY, (p1, p2) => p1.y == p2.y);

        public static IBinaryOperationState Vertically(Vector2Int position, IEnumerable<LogicBlock> operands)
            => new OperandsOnLine(position, operands, BinaryOperaionStateType.OPERANDS_VERTICALLY, (p1, p2) => p1.x == p2.x);

        public BinaryOperaionStateType StateType => _type;

        public bool CanAppend(Vector2Int operandPosition)
            => Map.GetVicinity(_position).Where(p => _rule(p, _position)).Contains(operandPosition);

        public BinaryOperaionStateType NextState(Vector2Int operandPosition)
        {
            if(_operands.Count() > 1)
                return BinaryOperaionStateType.ALL_OPERANDS_ADDED;
            return _type;
        }
    }

}