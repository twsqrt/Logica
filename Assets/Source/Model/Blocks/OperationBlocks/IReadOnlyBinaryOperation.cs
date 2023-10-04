namespace Model.BlocksLogic.OperationBlocksLogic
{
    public interface IReadOnlyBinaryOperation : IReadOnlyOperationBlock
    {
        Block FirstOperand { get; }
        Block SecondOperand { get; }
    }
}