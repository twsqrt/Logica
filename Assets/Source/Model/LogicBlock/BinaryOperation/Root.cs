using System;
using System.Linq;
using Model.LogicBlockLogic.BinaryOperationLogic;
using Model.MapLogic;
using UnityEngine;

namespace Model.LogicBlockLogic.BinaryOperationLogic
{
    public class Root : IBinaryOperationState
    {
        private readonly Vector2Int _position;
        public Root(Vector2Int position)
        {
            _position = position;
        }

        public BinaryOperaionStateType StateType => BinaryOperaionStateType.ROOT;

        public bool CanAppend(Vector2Int operandPosition)
            => Map.GetVicinity(_position).Contains(operandPosition);

        public BinaryOperaionStateType NextState(Vector2Int operandPosition)
        {
            if(_position.x == operandPosition.x)
                return BinaryOperaionStateType.OPERANDS_VERTICALLY;
            return BinaryOperaionStateType.OPERANDS_HORIZONTALLY;

        }
    }
}