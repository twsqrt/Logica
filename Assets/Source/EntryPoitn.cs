using Config;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using Model.BuilderLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using Presenter.BuilderLogic;
using UnityEngine;
using View.BlockLogic;
using View.BuilderLogic;
using View.InventoryLogic;
using View.MapLogic;
using Veiw.BuilderLogic;

namespace EntryPointLogic
{
    public class EntryPoitn : MonoBehaviour
    {
        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private ParameterConfig _parameterConfig;
        [SerializeField] private BlockUIViewFactory _blockUIViewFactory;
        [SerializeField] private BlockViewFactory _blockViewFactory;
        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private RemovingButton _removingButton;
        [SerializeField] private InventoryView _inventoryView;

        private void Awake()
        {
            Map map = new Map(_mapConfig);
            _mapView.Init(map);

            _blockUIViewFactory.Init(_parameterConfig);
            _blockViewFactory.Init(_parameterConfig);

            var blockFactory = new BlockFactory();
            var inventoryBuilder = new InventoryBuilder();

            Inventory inventory = inventoryBuilder.StartBuilding(blockFactory)
                .Register(new OperationData(LogicOperationType.NOT), 10)
                .Register(new OperationData(LogicOperationType.OR), 5)
                .Register(new OperationData(LogicOperationType.AND), 5)
                .RegisterInfinity(new ParameterData(1))
                .RegisterInfinity(new ParameterData(2))
                .RegisterInfinity(new ParameterData(3))
                .Build();
            
            BlockBuilder builder = new BlockBuilder(map, inventory);

            BuilderPlacingPresenter placingPresenter = new BuilderPlacingPresenter(builder);
            BuilderRemovingPresenter removingPresenter = new BuilderRemovingPresenter(builder);

            BuilderPresenter builderPresenter = new BuilderPresenter(map, placingPresenter, removingPresenter);

            _builderView.Init(_mapView, builderPresenter);
            _removingButton.Init(builderPresenter, removingPresenter);
            _inventoryView.Init(inventory, builderPresenter, placingPresenter);
        }
    }
}