using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public abstract class BinaryOperationState
    {
        private readonly Direction _operandCorrectDirections;

        public BinaryOperationState(Direction operandCorrectDirections)
        {
            _operandCorrectDirections = operandCorrectDirections;
        }

        public bool IsAppendCorrect(Direction direction)
            => (_operandCorrectDirections & direction) != 0;

        public abstract BinaryOperationStateType StateType { get; }
        public abstract BinaryOperationStateType NextState(Direction direction);
    }
}