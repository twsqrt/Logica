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
using Presenter;
using Model;
using Converter;

namespace EntryPointLogic
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private CoroutineTarget _coroutineTarget;

        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private TreeConfig _treeConfig;
        [SerializeField] private ParametersConfig _parametersConfig;
        [SerializeField] private FormulaRuleConfig _formulaRuleConfig;

        [SerializeField] private BlockUIViewFactory _blockUIViewFactory;
        [SerializeField] private BlockViewFactory _blockViewFactory;

        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private RemovingButton _removingButton;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PlayerFormulaView _playerFormulaView;
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
            
            var placingPresenter = new  PlacingPresenter(map, inventory, _treeConfig);
            var removingPresenter = new RemovingPresenter(map);
            var builderPresenter = new BuilderPresenter(map, placingPresenter, removingPresenter);

            var tree = new BlockTree(_treeConfig, map);
            var playerFormulaPresenter = new PlayerFormulaPresenter(tree, _parametersConfig, _coroutineTarget);
    
            var configStringToDelegate = new ConfigStringToDelegate();
            var TreeToDelegate = new TreeToDelegate(_parametersConfig);
            var formulaRule = new FormulaRule(_formulaRuleConfig, _parametersConfig, configStringToDelegate, TreeToDelegate);

            var executionPresenter = new ExecutionPresenter(tree, formulaRule);

            _playerFormulaView.Init(playerFormulaPresenter);
            _builderView.Init(_mapView, builderPresenter);
            _removingButton.Init(builderPresenter, removingPresenter);
            _inventoryView.Init(inventory, builderPresenter, placingPresenter);
            _executionButton.Init(tree, executionPresenter);
        }
    }
}