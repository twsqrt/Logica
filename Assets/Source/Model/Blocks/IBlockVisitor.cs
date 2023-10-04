using Model.BlocksLogic.OperationBlocksLogic;

namespace Model.BlocksLogic
{
    public interface IBlockVisitor<T>
    {
        T Visit(OperationNot operationNot);
        
        T Visit(BinaryOperation binaryOperation);

        T Visit(ParameterBlock parameter);
    }
}