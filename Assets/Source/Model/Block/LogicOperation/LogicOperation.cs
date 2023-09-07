using UnityEngine;
namespace Model.BlockLogic.LogicOperationLogic
{
    public abstract class LogicOperation : Block
    {
        protected readonly LogicOperationType _operationType;

        public LogicOperationType OperationType => _operationType;

        protected LogicOperation(LogicOperationType operationType, BlockContext context) 
        : base(operationType.ToBlockType(), context)
        {
            _operationType = operationType;
        }
    }
}