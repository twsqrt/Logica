using System;
using System.Collections.Generic;
using System.Linq;
using Model.BlocksLogic.BlocksData;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.InventoryLogic;

namespace Model.LevelTasksLogic
{
    public class AmountSaveTaskBuilder
    {
        private Dictionary<IBlockData, int> _registeredLimits;

        public AmountSaveTaskBuilder()
        {
            _registeredLimits = new Dictionary<IBlockData, int>();
        }

        public AmountSaveTaskBuilder StartBuilding()
        {
            _registeredLimits.Clear();
            return this;
        }

        public AmountSaveTaskBuilder RegisterOperation(LogicOperationType operationType, int limit)
        {
            _registeredLimits.Add(new OperationData(operationType), limit);
            return this;
        }

        public AmountSaveTaskBuilder RegisterParameter(int parameterId, int limit)
        {
            _registeredLimits.Add(new ParameterData(parameterId), limit);
            return this;
        }

        public AmountSaveTask Build()
            => new AmountSaveTask(_registeredLimits.ToDictionary(p => p.Key, p => p.Value));

        internal object RegisterOperation(object nOT, int v)
        {
            throw new NotImplementedException();
        }
    }
}