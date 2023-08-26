using Model.BlockLogic.LogicOperationLogic;

namespace Model.BlockLogic.BlockDataLogic
{
    public class OperationData : IBlockData
    {
        private readonly LogicOperationType _type;

        public LogicOperationType Type => _type;

        public OperationData(LogicOperationType type)
        {
            _type = type;
        }

        public Block AcceptFactory(BlockFactory facotry)
            => facotry.Create(this);
    }
}