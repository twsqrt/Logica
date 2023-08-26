using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public class AllOperandsAdded : IBinaryOperationState
    {
        public BinaryOperaionStateType StateType => BinaryOperaionStateType.ALL_OPERANDS_ADDED;

        public bool CanAppend(Vector2Int operandPosition) => false;

        public BinaryOperaionStateType NextState(Vector2Int operandPosition)
            => BinaryOperaionStateType.ALL_OPERANDS_ADDED;
    }
}