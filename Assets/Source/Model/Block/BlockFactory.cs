using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;

namespace Model.BlockLogic
{
    public class BlockFactory 
    {
        private readonly FactoryVisitor _factoryVisitor;
        private class FactoryVisitor : IBlockDataBasedFactory<Block>
        {
            public BlockPositionContext PositionContext;

            public Block Create(OperationData data)
            {
                LogicOperationType operationType = data.OperationType;
                if(operationType == LogicOperationType.NOT)
                    return new OperationNot(PositionContext);
                else
                    return new BinaryOperaion(operationType, PositionContext);
            }

            public Block Create(ParameterData data)
                => new Parameter(data.Id, PositionContext);
        }

        public BlockFactory()
        {
            _factoryVisitor = new FactoryVisitor();
        }

        public Block Create(IBlockData blockData, BlockPositionContext positionContext)
        {
            _factoryVisitor.PositionContext = positionContext;
            return blockData.AcceptFactory(_factoryVisitor);
        }
    }
    }