using Model.BlocksLogic.OperationBlocksLogic;

namespace Model.BlocksLogic.BlocksData
{
    public readonly struct OperationData : IBlockData
    {
        private readonly LogicOperationType _operationType;

        public LogicOperationType OperationType => _operationType;

        public OperationData(LogicOperationType operationType)
        {
            _operationType = operationType;
        }

        public T AcceptFactory<T>(IBlockDataBasedFactory<T> factory)
            => factory.Create(this);
    }
}