namespace Model.BlockLogic
{
    public enum BlockType
    {
        NONE = 0,
        PARAMETER = 1,
        OPERATION_NOT = 2,
        OPERATION_OR = 4,
        OPERATION_AND = 8,
        OPERATION_XOR = 16,
        OPERATION_NOR = 32,

        LOGIC_OPERATION = 62,
        BINARY_OPERATION = 60,
    }
}