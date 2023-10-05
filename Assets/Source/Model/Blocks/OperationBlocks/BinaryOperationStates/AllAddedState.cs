namespace Model.BlocksLogic.OperationBlocksLogic.BinaryOperationStatesLogic
{
    public class AllAddedState : BinaryOperationState
    {
        public AllAddedState() : base(Direction.NONE) {}

        public override BinaryOperationStateType StateType => BinaryOperationStateType.ALL_OPERANDS_ADDED;

        public override BinaryOperationStateType NextState(Direction direction)
            => BinaryOperationStateType.ALL_OPERANDS_ADDED;
    }
}