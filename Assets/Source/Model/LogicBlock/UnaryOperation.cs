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

        public override bool TryAppend(LogicBlock operand)
        {
            if(_operand != null)
                return false;

            if(Map.GetVicinity(_position).Contains(operand.Position) == false)
                return false;

            _operand = operand;
            operand.OnRemove += _ => OnRemoveHandler();

            return true;
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