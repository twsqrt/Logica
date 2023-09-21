using Config;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using Model.TreeLogic;
using Presenter.BuilderLogic;
using Presenter;
using UnityEngine;
using Veiw.BuilderLogic;
using View.BlockLogic;
using View.BuilderLogic;
using View.InventoryLogic;
using View.MapLogic;
using View;

namespace EntryPointLogic
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private ParameterBlocksConfig _parameterBlocksConfig;
        [SerializeField] private BlockUIViewFactory _blockUIViewFactory;
        [SerializeField] private BlockViewFactory _blockViewFactory;
        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private RemovingButton _removingButton;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PlayerFormulaView _formulaView;
        [SerializeField] private ExecutionButton _executionButton;

        private void Awake()
        {
            var map = new Map(_mapConfig);
            _mapView.Init(map);

            _blockUIViewFactory.Init(_parameterBlocksConfig);
            _blockViewFactory.Init(_parameterBlocksConfig);

            var blockFactory = new BlockFactory();
            var inventoryBuilder = new InventoryBuilder();

            Inventory inventory = inventoryBuilder.StartBuilding(blockFactory)
                .RegisterOperation(LogicOperationType.NOT, 10)
                .RegisterOperation(LogicOperationType.OR, 5)
                .RegisterOperation(LogicOperationType.AND, 5)
                .RegisterParameterInfinity(1)
                .RegisterParameterInfinity(2)
                .RegisterParameterInfinity(3)
                .Build();
            
            var placingPresenter = new PlacingPresenter(map, inventory);
            var removingPresenter = new RemovingPresenter(map);

            var builderPresenter = new BuilderPresenter(map, placingPresenter, removingPresenter);

            var treeStringConverter = new TreeToStringConverter(_parameterBlocksConfig);
            var formulaPresenter = new PlayerFormulaPresenter(map, treeStringConverter);
            
            var treeVerifier = new TreeVerifier();
            var executionPresenter = new ExecutionPresenter(map, treeVerifier);


            _formulaView.Init(formulaPresenter, builderPresenter);
            _builderView.Init(_mapView, builderPresenter);
            _removingButton.Init(builderPresenter, removingPresenter);
            _inventoryView.Init(inventory, builderPresenter, placingPresenter);
            _executionButton.Init(builderPresenter, executionPresenter);
        }
    }
}