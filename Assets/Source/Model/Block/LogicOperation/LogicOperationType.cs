using Model.BlockLogic;

namespace Model.BlockLogic.LogicOperationLogic
{
    public enum LogicOperationType
    {
        NOT = 2,
        OR = 4,
        AND = 8,
        XOR = 16,
        NOR = 32, 
    }

    public static class LogicOperationTypeExtension
    {
        public static BlockType ToBlockType(this LogicOperationType operationType)
            => (BlockType) operationType;
    }
}