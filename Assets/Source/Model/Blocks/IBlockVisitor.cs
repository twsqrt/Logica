using Model.BlocksLogic.OperationBlocksLogic;

namespace Model.BlocksLogic
{
    public interface IBlockVisitor<T>
    {
        T Visit(IReadOnlyOperationNot operationNot);
        
        T Visit(IReadOnlyBinaryOperation binaryOperation);

        T Visit(IReadOnlyParameterBlock parameter);
    }
}