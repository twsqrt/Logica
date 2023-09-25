using System;
using System.Collections.Generic;
using Config;
using Model.BlockLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;

namespace Model.TreeLogic
{
    public class TreeStringValue : TreeCacheValue<string>
    {
        private const char DEFAULT_OPERAND_SYMBOL = '_';
        private readonly Dictionary<int, string> _parametersName;

        private bool IsBracketsNeded(BlockType operation, Block operand)
        {
            if(operand == null)
                return false;

            BlockType blocksThatNeedBrackets = operation switch
            {
                BlockType.OPERATION_NOT => BlockType.BINARY_OPERATION,
                BlockType.OPERATION_OR => BlockType.OPERATION_XOR | BlockType.OPERATION_NOR,
                BlockType.OPERATION_AND => BlockType.OPERATION_OR | BlockType.OPERATION_XOR | BlockType.OPERATION_NOR,
                BlockType.OPERATION_XOR => BlockType.OPERATION_OR | BlockType.OPERATION_NOR,
                BlockType.OPERATION_NOR => BlockType.OPERATION_OR | BlockType.OPERATION_XOR,
                _ => BlockType.NONE,
            };

            return (blocksThatNeedBrackets & operand.BlockType) != 0;
        }

        protected override string DecorateOperandResult(Block operation, Block operand, string operandResult)
            => IsBracketsNeded(operation.BlockType, operand) ? $"({operandResult})" : operandResult; 

        protected override string GetNullValue()
            => DEFAULT_OPERAND_SYMBOL.ToString();

        protected override string GetParameterValue(Parameter parameter)
            => _parametersName[parameter.Id];

        protected override string Merge(BinaryOperation binaryOperation, string firstOperandResult, string secondOperandResult)
        {
            LogicOperationType operationType = binaryOperation.OperationType;
            char operationSymbol = operationType switch
            {
                LogicOperationType.OR => '\u2228',
                LogicOperationType.AND => '\u2227',
                LogicOperationType.XOR => '\u2295',
                LogicOperationType.NOR => '\u2191',
                _ => throw new ArgumentException($"Binary operation {operationType} not found!"),
            };

            return $"{firstOperandResult} {operationSymbol} {secondOperandResult}";
        }

        protected override string Merge(OperationNot operationNot, string operandResult)
            => '\u00AC' + operandResult;

        public TreeStringValue(BlockTree tree, ParametersConfig parametersConfig) : base(tree, string.Empty)
        {
            _parametersName = parametersConfig.GetParameterNameByIdDictionary();
        }
    }
}