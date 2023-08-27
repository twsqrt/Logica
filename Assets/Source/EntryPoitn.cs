using UnityEngine;
using Model.MapLogic;
using View.MapLogic;
using View.BuilderLogic;
using Config;
using Presenter;
using Model;
using Model.BlockLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.BlockDataLogic;
using Model.InventoryLogic;

namespace EntryPointLogic
{
    public class EntryPoitn : MonoBehaviour
    {
        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;

        private void Awake()
        {
            Map map = new Map(_mapConfig);
            _mapView.Init(map);

            var blockFactory = new BlockFactory();
            var inventoryBuilder = new InventoryBuilder();

            Inventory inventory = inventoryBuilder.StartBuilding(blockFactory)
                .Register(new OperationData(LogicOperationType.OR), 10)
                .Build();
            
            TreeBlockBuilder builder = new TreeBlockBuilder(map, inventory);
            BuilderPresenter builderPresenter = new BuilderPresenter(builder);
            _builderView.Init(_mapView, builderPresenter, builder);
        }
    }
}