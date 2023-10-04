namespace Model.BlocksLogic.OperationBlocksLogic
{
    public abstract class OperationBlock : Block, IReadOnlyOperationBlock
    {
        protected readonly OperationBlockType _operationType;

        public OperationBlockType OperationType => _operationType;

        protected OperationBlock(OperationBlockType operationType, BlockContext context) 
        : base(operationType.ToBlockType(), context)
        {
            _operationType = operationType;
        }
    }
}