using Converter;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using System.Collections.Generic;
using System;

namespace Model.TreeLogic
{
    public abstract class TreeCacheValue<T>
    {
        private class ConverterVisitor : IBlockVisitor<T>
        {
            private readonly TreeCacheValue<T> _treeCacheValue;
            private readonly Dictionary<Block, BlockCacheValue<T>> _blocksCache;
            
            private void AddCache(Block block)
            {
                _blocksCache.Add(block, new BlockCacheValue<T>(block, b => b.Accept(this)));
                block.OnDestroy += _ => _blocksCache.Remove(block);
            }

            private T ConvertAndDecorate(Block operation, Block operand)
            {
                T result = Converter(operand);
                return _treeCacheValue.DecorateOperandResult(operation, operand, result);
            }

            public ConverterVisitor(TreeCacheValue<T> treeCacheValue)
            {
                _treeCacheValue = treeCacheValue;
                _blocksCache = new Dictionary<Block, BlockCacheValue<T>>();
            }

            public T Visit(OperationNot operationNot)
            {
                Block operand = operationNot.Operand;
                
                T operandResult = ConvertAndDecorate(operationNot, operand);
                return _treeCacheValue.Merge(operationNot, operandResult);
            }

            public T Visit(BinaryOperation binaryOperation)
            {
                Block firstOperand = binaryOperation.FirstOperand;
                Block secondOperand = binaryOperation.SecondOperand;

                T firstOperandResult = ConvertAndDecorate(binaryOperation, firstOperand);
                T secondOperandResult = ConvertAndDecorate(binaryOperation, secondOperand);
                return _treeCacheValue.Merge(binaryOperation, firstOperandResult, secondOperandResult);
            }

            public T Visit(Parameter parameter)
                => _treeCacheValue.GetParameterValue(parameter);

            public T Converter(Block block)
            {
                if(block == null)
                    return _treeCacheValue.GetNullValue();

                if(_blocksCache.ContainsKey(block) == false)
                    AddCache(block);

                return _blocksCache[block].GetValue();
            }
        }

        private readonly BlockTree _tree;
        private readonly T _emptyTreeValue;
        private readonly ConverterVisitor _converterVisitor;

        protected virtual T DecorateOperandResult(Block operation, Block operand, T operandResult)
            => operandResult;
        protected virtual T GetNullValue() 
            => throw new ArgumentNullException();
        protected abstract T GetParameterValue(Parameter parameter);
        protected abstract T Merge(BinaryOperation binaryOperation, T firstOperandResult, T secondOperandResult);
        protected abstract T Merge(OperationNot operationNot, T operandResult);

        public TreeCacheValue(BlockTree tree, T emptyTreeValue = default)
        {
            _tree = tree;
            _emptyTreeValue = emptyTreeValue;
            _converterVisitor = new ConverterVisitor(this);
        }

        public T GetValue()
        {
            if(_tree.IsEmpty) 
                return _emptyTreeValue;
            return _converterVisitor.Converter(_tree.CurrentRoot);
        }
    }
}