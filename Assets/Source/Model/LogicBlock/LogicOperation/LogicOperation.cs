using UnityEngine;
namespace Model.LogicBlockLogic.LogicOperationLogic
{
    public abstract class LogicOperation : LogicBlock
    {
        private readonly LogicOperationType _type;

        public LogicOperationType OperationType => _type;

        protected LogicOperation(LogicOperationType type, Vector2Int position, LogicBlock parent) : base(position, parent)
        {
            _type = type;
        }
    }
}