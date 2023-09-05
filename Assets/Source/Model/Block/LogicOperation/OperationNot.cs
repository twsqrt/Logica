using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic
{
    public class OperationNot : LogicOperation
    {
        private Block _operand;

        public OperationNot(BlockPositionContext context) : base(LogicOperationType.NOT, context)
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
            return Map.GetVicinity(_context.Position).Contains(operandPosition);
        }

        public override void Append(Block operand)
        {
            _operand = operand;
            operand.OnRemove += _ => OnRemoveHandler();
        }

        public override bool CanBeRemoved()
            => _operand == null;

        public override T Accept<T>(IBlockVisitor<T> visitor)
            => visitor.Visit(this);
    }
}