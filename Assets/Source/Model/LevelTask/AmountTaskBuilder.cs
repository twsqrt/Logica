using System.Collections.Generic;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.InventoryLogic;
using View.BlockLogic.ViewDataLogic;

namespace Model.LevelTaskLogic
{
    public class AmountTaskBuilder
    {
        private Dictionary<IBlockData, int> _registeredLimits;

        public AmountTaskBuilder()
        {
            _registeredLimits = new Dictionary<IBlockData, int>();
        }

        public AmountTaskBuilder RegisterOperation(LogicOperationType operationType, int limit)
        {
            _registeredLimits.Add(new OperationData(operationType), limit);
            return this;
        }

        public AmountTaskBuilder RegisterParameter(int parameterId, int limit)
        {
            _registeredLimits.Add(new ParameterData(parameterId), limit);
            return this;
        }

        public AmountTask Build(Inventory inventory)
            => new AmountTask(inventory, _registeredLimits);
    }
}