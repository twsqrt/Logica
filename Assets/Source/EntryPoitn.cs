using Config;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using Model;
using Presenter.BuilderLogic;
using UnityEngine;
using View.BuilderLogic;
using View.MapLogic;
using View.InventoryLogic;
using View.BlockLogic;
using System.Reflection;

namespace EntryPointLogic
{
    public class EntryPoitn : MonoBehaviour
    {
        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private InventoryView _inventoryView;

        private void Awake()
        {
            Map map = new Map(_mapConfig);
            _mapView.Init(map);

            var blockFactory = new BlockFactory();
            var inventoryBuilder = new InventoryBuilder();

            Inventory inventory = inventoryBuilder.StartBuilding(blockFactory)
                .Register(new OperationData(LogicOperationType.OR), 5)
                .RegisterInfinity(new OperationData(LogicOperationType.NOT))
                .Build();
            
            TreeBlockBuilder builder = new TreeBlockBuilder(map, inventory);
            BuilderPresenter builderPresenter = new BuilderPresenter(builder);

            _builderView.Init(_mapView, builderPresenter, builder);
            _inventoryView.Init(builderPresenter, inventory);
        }
    }
}