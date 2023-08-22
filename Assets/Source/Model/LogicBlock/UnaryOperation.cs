using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.LogicBlockLogic
{
    public class UnaryOperation : LogicBlock
    {
        private LogicBlock _operand;

        public UnaryOperation(Vector2Int position) : base(position)
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