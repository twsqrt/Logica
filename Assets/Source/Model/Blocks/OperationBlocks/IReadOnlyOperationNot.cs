namespace Model.BlocksLogic.OperationBlocksLogic
{
    public interface IReadOnlyOperationNot : IReadOnlyOperationBlock
    {
        Block Operand { get; }
    }
}