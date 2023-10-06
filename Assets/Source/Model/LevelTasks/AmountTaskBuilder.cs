using System;
using System.Collections.Generic;
using System.Linq;
using Model.BlocksLogic.BlocksData;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.InventoryLogic;

namespace Model.LevelTasksLogic
{
    public class AmountTaskBuilder
    {
        private Dictionary<IBlockData, int> _registeredLimits;

        public AmountTaskBuilder()
        {
            _registeredLimits = new Dictionary<IBlockData, int>();
        }

        public AmountTaskBuilder StartBuilding()
        {
            _registeredLimits.Clear();
            return this;
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

        public AmountTask Build()
            => new AmountTask(_registeredLimits.ToDictionary(p => p.Key, p => p.Value));

        internal object RegisterOperation(object nOT, int v)
        {
            throw new NotImplementedException();
        }
    }
}