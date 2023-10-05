namespace Model.BlocksLogic.OperationBlocksLogic
{
    public interface IReadOnlyOperationBlock : IReadOnlyBlock
    {
        public OperationBlockType OperationType { get; }
    }
}