using Configs.LevelConfigs;
using Configs;
using Converter;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.BlocksLogic;
using Model.InventoryLogic;
using Model.LevelTasksLogic;
using Model.MapLogic;
using Model.TreeLogic;
using Presenter.Builder;
using Presenter;
using UnityEngine;
using View.Builder;
using View.InventoryLogic;
using View.LevelTasksLogic;
using View.MapLogic;
using View;
using Model.BlockLogic;

namespace EntryPointLogic
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ParameterNamesConfig _parametersConfig;

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
            var blockBuilder = new BlockBuilder(blockFactory, map);
            var inventory = new Inventory(blockBuilder, levelConfig.Inventory);
            
            var placingPresenter = new  PlacingPresenter(map, inventory, levelConfig.Tree);
            var removingPresenter = new RemovingPresenter(map);
            var builderPresenter = new BuilderPresenter(map, placingPresenter, removingPresenter);

            var tree = new BlockTree(levelConfig.Tree, map.AsReadOnly());
            var treeToViewString = new TreeToViewString(_parametersConfig);
            var playerFormulaPresenter = new PlayerFormulaPresenter(tree, treeToViewString);

            var treeToDelegate = new TreeToDelegate(levelConfig.Tasks.FormulaTask);
    
            var formulaTask = new FormulaTask(tree, levelConfig.Tasks.FormulaTask, new ConfigStringToDelegate(), treeToDelegate);
            var amountTaskBuilder = new AmountTaskBuilder();
            AmountTask amountTask2Stars = amountTaskBuilder
                .StartBuilding()
                .RegisterOperation(OperationBlockType.NOT, 8)
                .RegisterOperation(OperationBlockType.OR, 4)
                .Build(inventory);

            AmountTask amountTask3Stars = amountTaskBuilder
                .StartBuilding()
                .RegisterOperation(OperationBlockType.OR, 5)
                .Build(inventory);
            
            var levelTasksBuilder = new LevelTasksBuilder();
            LevelTasks levelTasks = levelTasksBuilder
                .StartBuilding()
                .RegisterForOneStar(formulaTask)
                .RegisterForTwoStars(amountTask2Stars)
                .RegisterForTreeStars(amountTask3Stars)
                .Build();

            var executionPresenter = new ExecutionPresenter(tree, levelTasks);

            _mapView.Init(map.AsReadOnly());
            _playerFormulaView.Init(tree, playerFormulaPresenter);
            _builderView.Init(_mapView, builderPresenter);
            _removingButton.Init(builderPresenter, removingPresenter);
            _inventoryView.Init(inventory, builderPresenter, placingPresenter);
            _executionButton.Init(tree, executionPresenter);
            _levelTasksView.Init(levelTasks);
        }
    }
}