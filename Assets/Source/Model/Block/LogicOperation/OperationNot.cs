using System.Linq;
using Model.MapLogic;
using UnityEngine;

namespace Model.BlockLogic.LogicOperationLogic
{
    public class OperationNot : LogicOperation
    {
        private Block _operand;

        public Block Operand => _operand;

        public OperationNot(BlockContext context) : base(LogicOperationType.NOT, context)
        {
            _operand = null;
        }

        private void OnRemoveHandler() 
            => _operand = null;

        public override bool CanAppend(BlockSide side)
            =>_operand == null;

        public override void Append(Block operand)
        {
            _operand = operand;
            operand.OnDestroy += _ => OnRemoveHandler();
        }

        public override bool HasOperands()
            => _operand != null;

        public override T Accept<T>(IBlockVisitor<T> visitor)
            => visitor.Visit(this);
    }
}