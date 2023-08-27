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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return _type == (obj as OperationData).Type;
        }
        
        public override int GetHashCode()
            => _type.GetHashCode();

        public Block AcceptFactory(BlockFactory facotry)
            => facotry.Create(this);
    }
}