using System.Collections.Generic;
using Model.BlockLogic.BlockDataLogic;
using Model.InventoryLogic.AmountLogic;
using Model.BlockLogic;
using Model.BlockLogic.LogicOperationLogic;

namespace Model.InventoryLogic
{
    public class InventoryBuilder
    {
        private Dictionary<IBlockData, IAmount> _registeredBlocks;
        private BlockFactory _factory;

        public InventoryBuilder()
        {
            _registeredBlocks = new Dictionary<IBlockData, IAmount>();
        }

        public InventoryBuilder StartBuilding(BlockFactory factory)
        {
            _registeredBlocks.Clear();
            _factory = factory;

            return this;
        }

        public InventoryBuilder Register(IBlockData data, int amount)
        {
            _registeredBlocks.Add(data, new ValueAmount(amount));
            return this;
        }

        public InventoryBuilder RegisterInfinity(IBlockData data)
        {
            _registeredBlocks.Add(data, new InfinityAmount());
            return this;
        }

        public InventoryBuilder RegisterOperation(LogicOperationType operationType, int amount)
            => Register(new OperationData(operationType), amount);
        
        public InventoryBuilder RegisterOperationInfinity(LogicOperationType operationType)
            => RegisterInfinity(new OperationData(operationType));
        
        public InventoryBuilder RegisterParameter(int parameterId, int amount)
            => Register(new ParameterData(parameterId), amount);

        public InventoryBuilder RegisterParameterInfinity(int parameterId)
            => RegisterInfinity(new ParameterData(parameterId));

        public Inventory Build()
        {
            return new Inventory(_factory, _registeredBlocks);
        }
    }
}