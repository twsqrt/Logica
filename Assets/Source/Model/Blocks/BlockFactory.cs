using Model.BlocksLogic.BlocksData;
using Model.BlocksLogic.OperationBlocksLogic;

namespace Model.BlocksLogic
{
    public class BlockFactory 
    {
        private readonly FactoryVisitor _factoryVisitor;
        private class FactoryVisitor : IBlockDataBasedFactory<Block>
        {
            public BlockContext PositionContext;

            public Block Create(OperationData data)
            {
                OperationBlockType operationType = data.OperationType;
                if(operationType == OperationBlockType.NOT)
                    return new OperationNot(PositionContext);
                else
                    return new BinaryOperation(operationType, PositionContext);
            }

            public Block Create(ParameterData data)
                => new Parameter(data.Id, PositionContext);
        }

        public BlockFactory()
        {
            _factoryVisitor = new FactoryVisitor();
        }

        public Block Create(IBlockData blockData, BlockContext positionContext)
        {
            _factoryVisitor.PositionContext = positionContext;
            return blockData.AcceptFactory(_factoryVisitor);
        }
    }
    }