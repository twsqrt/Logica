using Converter;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using System.Collections.Generic;

namespace Model.TreeLogic
{
    public abstract class TreeCacheValue<T>
    {
        private class ConverterVisitor : IBlockVisitor<T>, IConverter<Block, T>
        {
            private readonly TreeCacheValue<T> _TreeCacheValue;
            private readonly Dictionary<Block, BlockCacheValue<T>> _blocksCache;
            
            private void AddCache(Block block)
            {
                _blocksCache.Add(block, new BlockCacheValue<T>(block, b => b.Accept(this)));
                block.OnDestroy += _ => _blocksCache.Remove(block);
            }

            public ConverterVisitor(TreeCacheValue<T> TreeCacheValue)
            {
                _TreeCacheValue = TreeCacheValue;
                _blocksCache = new Dictionary<Block, BlockCacheValue<T>>();
            }

            public T Visit(OperationNot operationNot)
            {
                T operandResult = Converter(operationNot.Operand);
                return _TreeCacheValue.Merge(operationNot, operandResult);
            }

            public T Visit(BinaryOperation binaryOperation)
            {
                T firstOperandResult = Converter(binaryOperation.FirstOperand);
                T secondOperandResult = Converter(binaryOperation.SecondOperand);

                return _TreeCacheValue.Merge(binaryOperation, firstOperandResult, secondOperandResult);
            }

            public T Visit(Parameter parameter)
                => _TreeCacheValue.Converte(parameter);

            public T Converter(Block block)
            {
                if(_blocksCache.ContainsKey(block) == false)
                    AddCache(block);

                return _blocksCache[block].GetValue();
            }
        }

        private readonly BlockTree _tree;
        private readonly IBlockVisitor<T> _converterVisitor;

        protected abstract T Merge(BinaryOperation binaryOperation, T firstOperandResult, T secondOperandResult);
        protected abstract T Merge(OperationNot operationNot, T operandResult);
        protected abstract T Converte(Parameter parameter);

        public TreeCacheValue(BlockTree tree)
        {
            _tree = tree;
            _converterVisitor = new ConverterVisitor(this);
        }

        public T GetValue()
        {
            if(_tree.IsEmpty) return default;
            return _tree.CurrentRoot.Accept(_converterVisitor);
        }
    }
}