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
using Converter;
using Model.LevelTaskLogic;
using View.LevelTaskLogic;
using Newtonsoft.Json;
using System.IO;

namespace EntryPointLogic
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private TreeConfig _treeConfig;
        [SerializeField] private FormulaTaskConfig _FormulaTaskConfig;

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
            LevelConfig levelConfig = LevelConfigLoader.Load("level_0.json");

            var map = new Map(levelConfig.Map);

            _blockUIViewFactory.Init(levelConfig.Parameters);
            _blockViewFactory.Init(levelConfig.Parameters);

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
            
            var placingPresenter = new  PlacingPresenter(new VectorToDirection(), map, inventory, _treeConfig);
            var removingPresenter = new RemovingPresenter(map);
            var builderPresenter = new BuilderPresenter(map, placingPresenter, removingPresenter);

            var tree = new BlockTree(_treeConfig, map);
            var treeToViewString = new TreeToViewString(levelConfig.Parameters);
            var playerFormulaPresenter = new PlayerFormulaPresenter(tree, treeToViewString);

            var treeToDelegate = new TreeToDelegate(levelConfig.Parameters);

            var formulaTask = new FormulaTask(tree, _FormulaTaskConfig, levelConfig.Parameters, new ConfigStringToDelegate(), treeToDelegate);
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