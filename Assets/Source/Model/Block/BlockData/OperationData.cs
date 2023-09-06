using Model.BlockLogic.LogicOperationLogic;

namespace Model.BlockLogic.BlockDataLogic
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