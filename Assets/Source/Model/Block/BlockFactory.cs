using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using UnityEngine;

namespace Model.BlockLogic
{
    public class BlockFactory : IBlockDataBasedFactory<Block>
    {
        public BlockPositionContext CreationContext;

        public Block Create(OperationData data)
        {
            switch(data.Type)
            {
                case LogicOperationType.NOT:
                    return new OperationNot(CreationContext);
                default:
                    return new BinaryOperaion(data.Type, CreationContext);
            }
        }

        public Block Create(ParameterData data)
            => new Parameter(data.Id, CreationContext);
    }
}