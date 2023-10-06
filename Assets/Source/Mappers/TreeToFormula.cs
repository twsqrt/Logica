using Configs;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.BlocksLogic;
using Model.TreeLogic;
using System;

namespace Mappers
{
    public class TreeToFormula
    {
        private class Visitor : IBlockVisitor<string>
        {
            private const string NOT_SELECTED_OPERAND_TEXT = "_";
            private readonly FormulaConfig _formulaConfig;

            private bool IsBracketsNeded(BlockType operation, BlockType operand)
            {
                BlockType blocksThatNeedBrackets = operation switch
                {
                    BlockType.OPERATION_NOT => BlockType.BINARY_OPERATION,
                    BlockType.OPERATION_OR => BlockType.OPERATION_XOR | BlockType.OPERATION_NOR,
                    BlockType.OPERATION_AND => BlockType.OPERATION_OR | BlockType.OPERATION_XOR | BlockType.OPERATION_NOR,
                    BlockType.OPERATION_XOR => BlockType.OPERATION_OR | BlockType.OPERATION_NOR,
                    BlockType.OPERATION_NOR => BlockType.OPERATION_OR | BlockType.OPERATION_XOR,
                    _ => BlockType.NONE,
                };

                return (blocksThatNeedBrackets & operand) != 0;
            }

            private string GetOperandText(BlockType blockType, IReadOnlyBlock operand)
            {
                if(operand == null)
                    return NOT_SELECTED_OPERAND_TEXT;

                string operandText = operand.Accept(this);
                bool isBracketsNeded = IsBracketsNeded(blockType, operand.BlockType);
                return isBracketsNeded ? $"({operandText})" : operandText;
            }

            public Visitor(FormulaConfig formulaConfig)
            {
                _formulaConfig = formulaConfig;
            }

            public string Visit(IReadOnlyOperationNot operationNot)
                => '\u00AC' + GetOperandText(BlockType.OPERATION_NOT, operationNot.Operand);

            public string Visit(IReadOnlyBinaryOperation binaryOperation)
            {
                LogicOperationType operationType = binaryOperation.OperationType;
                char operationSymbol = _formulaConfig.GetOperationChar(operationType);
                BlockType blockType = binaryOperation.BlockType;
                
                string leftOperandText = GetOperandText(blockType, binaryOperation.FirstOperand);
                string rightOperandText = GetOperandText(blockType, binaryOperation.SecondOperand);

                return $"{leftOperandText} {operationSymbol} {rightOperandText}";
            }

            public string Visit(IReadOnlyParameterBlock parameter)
                => _formulaConfig.ParameterNames[parameter.Id];
        }

        private readonly Visitor _visitor;

        public TreeToFormula(FormulaConfig formulaConfig)
        {
            _visitor = new Visitor(formulaConfig);
        }
        
        public string Convert(BlockTree tree)
            => tree.IsEmpty ? string.Empty : tree.CurrentRoot.Accept(_visitor);
    }
}