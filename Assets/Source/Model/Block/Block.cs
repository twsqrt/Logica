using System;
using UnityEngine;

namespace Model.BlockLogic
{
    public abstract class Block
    {
        protected readonly BlockContext _context;
        protected readonly BlockType _blockType;

        public event Action<Block> OnDestroy;

        public BlockContext Context => _context;
        public BlockType BlockType => _blockType;

        public Block(BlockType blockType, BlockContext context)
        {
            _blockType = blockType;
            _context = context;
        }

        public void Destroy() => OnDestroy?.Invoke(this);

        public abstract bool HasOperands();
        public abstract bool IsAppendCorrect(Direction direction);
        public abstract void Append(Direction direction, Block operand);
        public abstract T Accept<T>(IBlockVisitor<T> visitor);
    }
}