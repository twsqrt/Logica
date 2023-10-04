using Configs.LevelConfigs.InventoryConfigs;
using Model.BlocksLogic.BlocksData;
using Model.BlocksLogic;
using Model.InventoryLogic.AmountLogic;
using System.Collections.Generic;
using UnityEngine;
using Model.BlockLogic;
using System;

namespace Model.InventoryLogic
{
    public class Inventory
    {
        private readonly Dictionary<IBlockData, IAmount> _amounts;
        private readonly BlockBuilder _builder;

        public IReadOnlyAmount this[IBlockData data]
        {
            get
            {
                if(_amounts.TryGetValue(data, out IAmount amount))
                    return amount;
                return ValueAmount.Zero; 
            }
        }

        public IEnumerable<IBlockData> AllBlocksData
            => _amounts.Keys;

        public Inventory(BlockBuilder builder, InventoryConfig config)
        {
            _builder = builder;

            _amounts = new Dictionary<IBlockData, IAmount>();
            foreach(InventorySlotConfig slot in config.Slots)
                _amounts.Add(slot.Data, AmountFactory.Create(slot.Amount));
        }

        public bool CanPullOut(IBlockData data, Vector2Int at)
            => _amounts.TryGetValue(data, out IAmount amount)
            && amount.MoreThan(0)
            && _builder.CanPlace(at);

        public void PullOut(IBlockData data, Vector2Int at)
        {
            if(_amounts.TryGetValue(data, out IAmount amount ) == false
                || amount.TryDecrease() == false)
                throw new InvalidOperationException();

            Block block = _builder.Place(data, at);
            block.OnDestroy += () => amount.Increase();
        }
    }
}