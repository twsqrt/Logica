using Model.InventoryLogic;
using Model.MapLogic;
using Model.TreeLogic;

namespace Model.LevelStateLogic
{
    public class LevelState
    {
        private readonly Map _map; 
        private readonly Inventory _inventory;
        private readonly BlockTree _tree;

        public LevelState(Map map, Inventory inventory, BlockTree tree) 
        {
            _map = map;
            _inventory = inventory;
            _tree = tree;
        }

        public ReadOnlyMap Map => _map.AsReadOnly();
        public IReadOnlyInventory Inventory => _inventory;
        public BlockTree Tree => _tree;
    }
}