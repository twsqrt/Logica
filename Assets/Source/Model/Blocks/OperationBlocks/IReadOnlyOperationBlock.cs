namespace Model.BlocksLogic.OperationBlocksLogic
{
    public interface IReadOnlyOperationBlock : IReadOnlyBlock
    {
        public LogicOperationType OperationType { get; }
    }
}