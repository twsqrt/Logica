namespace Model.BlocksLogic.OperationBlocksLogic
{
    public interface IReadOnlyBinaryOperation : IReadOnlyOperationBlock
    {
        IReadOnlyBlock FirstOperand { get; }
        IReadOnlyBlock SecondOperand { get; }
    }
}