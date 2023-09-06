using Model.BlockLogic.LogicOperationLogic;

namespace Model.BlockLogic.BlockDataLogic
{
    public class OperationData : IBlockData
    {
        private readonly LogicOperationType _operationType;

        public LogicOperationType OperationType => _operationType;

        public OperationData(LogicOperationType operationType)
        {
            _operationType = operationType;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return _operationType == (obj as OperationData).OperationType;
        }
        
        public override int GetHashCode()
            => _operationType.GetHashCode();

        public T AcceptFactory<T>(IBlockDataBasedFactory<T> factory)
            => factory.Create(this);
    }
}