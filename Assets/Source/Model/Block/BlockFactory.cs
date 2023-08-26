using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using UnityEngine;

namespace Model.BlockLogic
{
    public class BlockFactory
    {
        public BlockPositionContext CreationContext;

        public LogicOperation Create(OperationData data)
        {
            switch(data.Type)
            {
                case LogicOperationType.NOT:
                    return new OperationNot(CreationContext);
                default:
                    return new BinaryOperaion(data.Type, CreationContext);
            }
        }

        public Parameter Create(ParameterData data)
            => new Parameter(data.Id, CreationContext);
    }
}