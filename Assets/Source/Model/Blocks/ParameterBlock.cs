using System;

namespace Model.BlocksLogic
{
    public class ParameterBlock : Block, IReadOnlyParameterBlock
    {
        private readonly int _id;

        public int Id => _id;

        public ParameterBlock(int id, BlockContext context) : base(BlockType.PARAMETER, context)
        {
            _id = id;
        }

        public override bool IsAppendCorrect(Direction direction) 
            => false;

        public override void Append(Direction direction, Block operand)
            => throw new InvalidOperationException();

        public override bool HasOperands() 
            => false;

        public override T Accept<T>(IBlockVisitor<T> visitor)
            => visitor.Visit(this);
    }
}