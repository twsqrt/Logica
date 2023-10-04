using System;

namespace Model.BlocksLogic
{
    public abstract class Block : IReadOnlyBlock
    {
        protected readonly BlockContext _context;
        protected readonly BlockType _blockType;

        public event Action<Block> OnDestroy;
        public event Action OnSubTreeChanged;

        public BlockContext Context => _context;
        public BlockType BlockType => _blockType;

        public Block(BlockType blockType, BlockContext context)
        {
            _blockType = blockType;
            _context = context;
        }

        protected virtual void RemoveOperand(Block operand)
        {
            OnSubTreeChanged?.Invoke();
        }

        public void Destroy() => OnDestroy?.Invoke(this);

        public virtual void Append(Direction direction, Block operand)
        {
            operand.OnSubTreeChanged += () => OnSubTreeChanged?.Invoke();
            operand.OnDestroy += RemoveOperand;
            OnSubTreeChanged?.Invoke();
        }

        public abstract bool HasOperands();
        public abstract bool IsAppendCorrect(Direction direction);
        public abstract T Accept<T>(IBlockVisitor<T> visitor);
    }
}