namespace Model.BlocksLogic.OperationBlocksLogic
{
    public interface IReadOnlyOperationNot : IReadOnlyOperationBlock
    {
        IReadOnlyBlock Operand { get; }
    }
}