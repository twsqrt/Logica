using System.Collections.Generic;
using JetBrains.Annotations;
using Model.BlocksLogic.BlocksData;
using Model.InventoryLogic.AmountLogic;
using UnityEngine;

namespace Model.InventoryLogic
{
    public interface IReadOnlyInventory
    {
        IReadOnlyAmount this[IBlockData data] { get; }
        IEnumerable<IBlockData> AllBlocksData { get; }
        bool CanPullOut(IBlockData data, Vector2Int at);
    }
}