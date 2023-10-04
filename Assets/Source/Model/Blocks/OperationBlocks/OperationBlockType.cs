namespace Model.BlocksLogic.OperationBlocksLogic
{
    public enum OperationBlockType
    {
        NOT = 2,
        OR = 4,
        AND = 8,
        XOR = 16,
        NOR = 32, 
    }

    public static class OperationBlockTypeExtension
    {
        public static BlockType ToBlockType(this OperationBlockType operationType)
            => (BlockType) operationType;
    }
}