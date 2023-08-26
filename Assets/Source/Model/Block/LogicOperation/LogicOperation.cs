using UnityEngine;
namespace Model.BlockLogic.LogicOperationLogic
{
    public abstract class LogicOperation : Block
    {
        private readonly LogicOperationType _type;

        public LogicOperationType OperationType => _type;

        protected LogicOperation(LogicOperationType type, Vector2Int position, Block parent) : base(position, parent)
        {
            _type = type;
        }
    }
}