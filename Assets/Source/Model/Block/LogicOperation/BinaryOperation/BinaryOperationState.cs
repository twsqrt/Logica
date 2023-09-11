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

        public bool CanAppend(BlockSide side)
            => (_blockCorrectSides & side) != 0;

        public abstract BinaryOperaionStateType StateType { get; }
        public abstract BinaryOperaionStateType NextState(BlockSide side);
    }
}