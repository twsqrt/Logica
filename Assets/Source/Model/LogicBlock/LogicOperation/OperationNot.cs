using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.LogicBlockLogic.LogicOperationLogic
{
    public class OperationNot : LogicOperation
    {
        private LogicBlock _operand;

        public OperationNot(Vector2Int position, LogicBlock parent) : base(LogicOperationType.NOT, position, parent)
        {
            _operand = null;
        }

        private void OnRemoveHandler() => _operand = null;

        public override bool IsCorrectTree()
        {
            if(_operand == null)
                return false;
            return _operand.IsCorrectTree();
        }

        public override bool CanAppend(Vector2Int operandPosition)
        {
            if(_operand != null)
                return false;
            return Map.GetVicinity(_position).Contains(operandPosition);
        }

        public override void Append(LogicBlock operand)
        {
            _operand = operand;
            operand.OnRemove += _ => OnRemoveHandler();
        }

        public override bool TryRemove()
        {
            if(_operand != null)
                return false;
            
            OnRemoveInvoke();
            return true;
        }
    }
}