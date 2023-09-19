using Model.BlockLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;

namespace Model.TreeLogic
{
    public class TreeVerifier : IBlockVisitor<bool>
    {
        public bool Visit(OperationNot operationNot)
            => operationNot.HasOperands() && operationNot.Operand.Accept(this);
       

        public bool Visit(BinaryOperation binaryOperation)
        {
            Block firstOperand = binaryOperation.FirstOperand;
            Block secondOperand = binaryOperation.SecondOperand;

            return firstOperand != null && secondOperand != null
                && firstOperand.Accept(this) && secondOperand.Accept(this);
        }

        public bool Visit(Parameter parameter)
            => true;
    }
}