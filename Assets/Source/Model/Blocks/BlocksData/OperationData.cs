using Model.BlocksLogic.OperationBlocksLogic;

namespace Model.BlocksLogic.BlocksData
{
    public readonly struct OperationData : IBlockData
    {
        private readonly OperationBlockType _operationType;

        public OperationBlockType OperationType => _operationType;

        public OperationData(OperationBlockType operationType)
        {
            _operationType = operationType;
        }

        public T AcceptFactory<T>(IBlockDataBasedFactory<T> factory)
            => factory.Create(this);
    }
}