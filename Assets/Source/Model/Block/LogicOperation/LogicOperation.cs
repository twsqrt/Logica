using UnityEngine;
namespace Model.BlockLogic.LogicOperationLogic
{
    public abstract class LogicOperation : Block
    {
        private readonly LogicOperationType _type;

        public LogicOperationType OperationType => _type;

        protected LogicOperation(LogicOperationType type, BlockContext context) : base(context)
        {
            _type = type;
        }
    }
}