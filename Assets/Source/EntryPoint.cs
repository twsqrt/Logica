using Config.LevelLogic.LevelTaskLogic;
using Config;
using Converter;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using Model.InventoryLogic;
using Model.LevelTaskLogic;
using Model.MapLogic;
using Model.TreeLogic;
using Presenter.BuilderLogic;
using Presenter;
using UnityEngine;
using Veiw.BuilderLogic;
using View.BlockLogic;
using View.BuilderLogic;
using View.InventoryLogic;
using View.LevelTaskLogic;
using View.MapLogic;
using View;

namespace EntryPointLogic
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ParameterNamesConfig _parametersConfig;

        [SerializeField] private BlockUIViewFactory _blockUIViewFactory;
        [SerializeField] private BlockViewFactory _blockViewFactory;

        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private RemovingButton _removingButton;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PlayerFormulaView _playerFormulaView;
        [SerializeField] private ExecutionButton _executionButton;
        [SerializeField] private LevelTasksView _levelTasksView;

        private void Awake()
        {
            LevelConfig levelConfig = LevelConfigLoader.Load("level.json");

            var map = new Map(levelConfig.Map);
    
            var blockFactory = new BlockFactory();
            var inventory = new Inventory(blockFactory, levelConfig.Inventory);
            
            var placingPresenter = new  PlacingPresenter(new VectorToDirection(), map, inventory, levelConfig.Tree);
            var removingPresenter = new RemovingPresenter(map);
            var builderPresenter = new BuilderPresenter(map, placingPresenter, removingPresenter);

            var tree = new BlockTree(levelConfig.Tree, map);
            var treeToViewString = new TreeToViewString(_parametersConfig);
            var playerFormulaPresenter = new PlayerFormulaPresenter(tree, treeToViewString);

            var treeToDelegate = new TreeToDelegate(_parametersConfig);

            var formulaTask = new FormulaTask(tree, levelConfig.Tasks.FormulaTask, _parametersConfig, new ConfigStringToDelegate(), treeToDelegate);
            var amountTaskBuilder = new AmountTaskBuilder();
            AmountTask amountTask2Stars = amountTaskBuilder
                .StartBuilding()
                .RegisterOperation(LogicOperationType.NOT, 8)
                .RegisterOperation(LogicOperationType.OR, 4)
                .Build(inventory);

            AmountTask amountTask3Stars = amountTaskBuilder
                .StartBuilding()
                .RegisterOperation(LogicOperationType.OR, 5)
                .Build(inventory);
            
            var levelTasksBuilder = new LevelTasksBuilder();
            LevelTasks levelTasks = levelTasksBuilder
                .StartBuilding()
                .RegisterForOneStar(formulaTask)
                .RegisterForTwoStars(amountTask2Stars)
                .RegisterForTreeStars(amountTask3Stars)
                .Build();

            var executionPresenter = new ExecutionPresenter(tree, levelTasks);

            _mapView.Init(map);
            _playerFormulaView.Init(tree, playerFormulaPresenter);
            _builderView.Init(_mapView, builderPresenter);
            _removingButton.Init(builderPresenter, removingPresenter);
            _inventoryView.Init(inventory, builderPresenter, placingPresenter);
            _executionButton.Init(tree, executionPresenter);
            _levelTasksView.Init(levelTasks);
        }
    }
}