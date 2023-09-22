using Config;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using Model.InventoryLogic;
using Model.MapLogic;
using Model.TreeLogic;
using Presenter.BuilderLogic;
using UnityEngine;
using Veiw.BuilderLogic;
using View.BlockLogic;
using View.BuilderLogic;
using View.InventoryLogic;
using View.MapLogic;
using View;
using Model;

namespace EntryPointLogic
{
    public class EntryPoitn : MonoBehaviour
    {
        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private ParametersConfig _parametersConfig;
        [SerializeField] private BlockUIViewFactory _blockUIViewFactory;
        [SerializeField] private BlockViewFactory _blockViewFactory;
        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private RemovingButton _removingButton;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private FormulaView _formulaView;
        [SerializeField] private ExecutionButton _executionButton;

        private void Awake()
        {
            var map = new Map(_mapConfig);
            _mapView.Init(map);

            _blockUIViewFactory.Init(_parametersConfig);
            _blockViewFactory.Init(_parametersConfig);

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

            var treeStringConverter = new TreeToStringConverter(_parametersConfig);
            var tree = new BlockTree(map);

            var treeExpressionConverter = new TreeToExpressionConverter(_parametersConfig);
            var rule = new FormulaRule(null, _parametersConfig, treeExpressionConverter);

            _formulaView.Init(tree, builderPresenter, treeStringConverter);
            _builderView.Init(_mapView, builderPresenter);
            _removingButton.Init(builderPresenter, removingPresenter);
            _inventoryView.Init(inventory, builderPresenter, placingPresenter);
            _executionButton.Init(tree, builderPresenter);
            _executionButton.OnClick += () => Debug.Log(rule.Execute(map));
        }
    }
}