using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public interface IBinaryOperationState
    {
        public BinaryOperaionStateType StateType { get; }

        bool CanAppend(Vector2Int operandPosition);
        
        BinaryOperaionStateType NextState(Vector2Int operandPosition);
    }
}