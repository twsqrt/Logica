using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic
{
    public abstract class BinaryOperationState
    {
        private readonly BlockSide _blockCorrectSides;

        public BinaryOperationState(BlockSide blockCorrectSides)
        {
            _blockCorrectSides = blockCorrectSides;
        }

        public bool IsAppendCorrect(BlockSide side)
            => (_blockCorrectSides & side) != 0;

        public abstract BinaryOperationStateType StateType { get; }
        public abstract BinaryOperationStateType NextState(BlockSide side);
    }
}