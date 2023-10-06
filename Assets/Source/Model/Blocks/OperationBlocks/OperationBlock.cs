namespace Model.BlocksLogic.OperationBlocksLogic
{
    public abstract class OperationBlock : Block, IReadOnlyOperationBlock
    {
        protected readonly LogicOperationType _operationType;

        public LogicOperationType OperationType => _operationType;

        protected OperationBlock(LogicOperationType operationType, BlockContext context) 
        : base(operationType.ToBlockType(), context)
        {
            _operationType = operationType;
        }
    }
}