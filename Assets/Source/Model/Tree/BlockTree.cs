using Configs.LevelConfigs;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.BlocksLogic;
using Model.MapLogic;
using System;

namespace Model.TreeLogic
{
    public class BlockTree
    {
        private class Verifier : IBlockVisitor<bool>
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


        private readonly Verifier _verifier;
        private readonly MapTile _rootTile;

        private Block _currentRoot;

        public event Action OnChanged;

        public Block CurrentRoot => _currentRoot;
        public bool IsEmpty => _currentRoot == null;

        private void SetRoot(Block root)
        {
            _currentRoot = root;
            root.OnSubTreeChanged += () => OnChanged?.Invoke();
            OnChanged?.Invoke();
        }

        private void RemoveRoot()
        {
            _currentRoot = null;
            OnChanged?.Invoke();
        }

        public BlockTree(TreeConfig config, Map map)
        {
            _verifier = new Verifier();
            _rootTile = map[config.RootPosition];

            _rootTile.OnBlockPlaced += SetRoot;
            _rootTile.OnBlockRemoved += RemoveRoot;
        }

        public bool IsCorrect()
            => IsEmpty == false && _currentRoot.Accept(_verifier);
    }
}