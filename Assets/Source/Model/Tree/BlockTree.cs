using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using Model.MapLogic;

namespace Model.TreeLogic
{
    public class BlockTree
    {
        private class Visitor : IBlockVisitor<bool>
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


        private readonly Visitor _visitor;
        private readonly Map _map; 
        public Block Root => _map[_map.RootPosition].Block;
        public bool IsEmpty => Root == null;

        public BlockTree(Map map)
        {
            _visitor = new Visitor();
            _map = map;
        }

        public bool IsCorrect()
            => Root != null && Root.Accept(_visitor);
    }
}