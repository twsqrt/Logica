using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;

namespace Model.BlockLogic
{
    public interface IBlockVisitor<T>
    {
        T Visit(OperationNot operationNot);
        
        T Visit(BinaryOperaion binaryOperaion);

        T Visit(Parameter parameter);
    }
}