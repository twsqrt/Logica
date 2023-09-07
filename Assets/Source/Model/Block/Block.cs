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
        public Vector2Int Position => _context.Position;

        public Block(BlockType blockType, BlockContext positionContext)
        {
            _blockType = blockType;
            _context = positionContext;
        }

        public void Destroy() => OnDestroy?.Invoke(this);

        public abstract bool HasOperands();
        public abstract bool CanAppend(Vector2Int operandPosition);
        public abstract void Append(Block operand);
        public abstract bool IsCorrectTree();
        public abstract T Accept<T>(IBlockVisitor<T> visitor);
    }
}