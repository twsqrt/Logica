namespace Model.BlocksLogic.OperationBlocksLogic
{
    public class OperationNot : OperationBlock, IReadOnlyOperationNot
    {
        private Block _operand;

        public IReadOnlyBlock Operand => _operand;

        public OperationNot(BlockContext context) : base(LogicOperationType.NOT, context)
        {
            _operand = null;
        }

        protected override void RemoveOperand(Block operand)
        {
            _operand = null;
            base.RemoveOperand(operand);
        }


        public override bool IsAppendCorrect(Direction direction)
            =>_operand == null;

        public override void Append(Direction direction, Block operand)
        {
            _operand = operand;
            base.Append(direction, operand);
        }

        public override bool HasOperands()
            => _operand != null;

        public override T Accept<T>(IBlockVisitor<T> visitor)
            => visitor.Visit(this);
    }
}