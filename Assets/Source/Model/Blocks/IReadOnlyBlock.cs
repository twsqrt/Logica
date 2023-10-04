using System;

namespace Model.BlocksLogic
{
    public interface IReadOnlyBlock
    {
        event Action<Block> OnDestroy;
        event Action OnSubTreeChanged;

        BlockContext Context { get; }
        BlockType BlockType { get; }

        bool HasOperands();
        bool IsAppendCorrect(Direction direction);
        T Accept<T>(IBlockVisitor<T> visitor);
    }
}