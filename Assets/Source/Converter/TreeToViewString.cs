using Configs;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.BlocksLogic;
using Model.TreeLogic;
using System;

namespace Converter
{
    public class TreeToViewString : IConverter<BlockTree, string>
    {
        private class Visitor : IBlockVisitor<string>
        {
            private const char DEFAULT_OPERAND_SYMBOL = '_';
            private readonly ParameterNamesConfig _parametersConfig;

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

            private string GetOperandText(BlockType operationType, Block operand)
            {
                if(operand == null)
                    return DEFAULT_OPERAND_SYMBOL.ToString();

                string operandText = operand.Accept(this);
                bool isBracketsNeded = IsBracketsNeded(operationType, operand.BlockType);
                return isBracketsNeded ? $"({operandText})" : operandText;
            }

            public Visitor(ParameterNamesConfig parametersConfig)
            {
                _parametersConfig = parametersConfig;
            }

            public string Visit(OperationNot operationNot)
                => '\u00AC' + GetOperandText(BlockType.OPERATION_NOT, operationNot.Operand);

            public string Visit(BinaryOperation binaryOperation)
            {
                OperationBlockType binaryOperationType = binaryOperation.OperationType;
                char operationSymbol = binaryOperationType switch
                {
                    OperationBlockType.OR => '\u2228',
                    OperationBlockType.AND => '\u2227',
                    OperationBlockType.XOR => '\u2295',
                    OperationBlockType.NOR => '\u2191',
                    _ => throw new ArgumentException($"Binary operation {binaryOperationType} not found!"),
                };

                BlockType operationType = binaryOperationType.ToBlockType();
                string leftOperandText = GetOperandText(operationType, binaryOperation.FirstOperand);
                string rightOperandText = GetOperandText(operationType, binaryOperation.SecondOperand);

                return $"{leftOperandText} {operationSymbol} {rightOperandText}";
            }

            public string Visit(ParameterBlock parameter)
                => _parametersConfig[parameter.Id];
        }

        private readonly Visitor _visitor;

        public TreeToViewString(ParameterNamesConfig parametersConfig)
        {
            _visitor = new Visitor(parametersConfig);
        }
        
        public string Convert(BlockTree tree)
            => tree.IsEmpty ? string.Empty : tree.CurrentRoot.Accept(_visitor);
    }
}