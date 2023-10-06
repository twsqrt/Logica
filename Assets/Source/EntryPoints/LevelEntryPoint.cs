using Configs.LevelConfigs;
using Mappers;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.BlocksLogic;
using Model.InventoryLogic;
using Model.LevelTasksLogic;
using Model.MapLogic;
using Model.TreeLogic;
using Presenter.Builder;
using Presenter;
using System;
using UnityEngine;
using View.Builder;
using View.InventoryLogic;
using View.LevelTasksLogic;
using View.MapLogic;
using View;
using Zenject;
using Model.BlockLogic;
using UnityEngine.Rendering;

namespace EntryPoints
{
    public class LevelEntryPoint : MonoInstaller
    {
        [SerializeField] private LevelTaskViewFactory _taskViewFactory;

        [SerializeField] private MapView _mapView;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private RemovingButton _removingButton;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PlayerFormulaView _playerFormulaView;
        [SerializeField] private ExecutionButton _executionButton;
        [SerializeField] private LevelTasksView _levelTasksView;

        private void BindConfigs()
        {
            LevelConfig levelConfig = LevelConfigLoader.Load("level");

            Container.BindInstance(levelConfig.Map);
            Container.BindInstance(levelConfig.Inventory);
            Container.BindInstance(levelConfig.Tree);
            Container.BindInstance(levelConfig.Tasks.FormulaTask);
        }

        private void BindMappers()
        {
            Container.Bind<FormulaMapper>().AsSingle();
            Container.Bind<DelegateMapper>().AsSingle();
        }

        private void BindModel()
        {

            Container.Bind<Map>().AsSingle();
            Container.Bind<ReadOnlyMap>().AsSingle();

            Container.Bind<BlockFactory>().AsSingle();
            Container.Bind<BlockBuilder>().AsSingle();
            Container.Bind<Inventory>().AsSingle();

            Container.Bind<BlockTree>().AsSingle();
            Container.Bind<FormulaTask>().AsSingle();
        }

        private void BindPresenters()
        {
            Container.Bind<PlacingPresenter>().AsSingle();
            Container.Bind<RemovingPresenter>().AsSingle();
            Container.Bind<BuilderPresenter>().AsSingle();

            Container.Bind<PlayerFormulaPresenter>().AsSingle();
            Container.Bind<ExecutionPresenter>().AsSingle();
        }

        private void BindView()
        {
            Container.BindInstance(_mapView).NonLazy();
            Container.BindInstance(_builderView).NonLazy();
            Container.BindInstance(_inventoryView).NonLazy();
            Container.BindInstance(_playerFormulaView).NonLazy();
            Container.BindInstance(_levelTasksView).NonLazy();

            Container.BindInstance(_removingButton).NonLazy();
            Container.BindInstance(_executionButton).NonLazy();
        }

        public override void InstallBindings()
        {
            BindConfigs();
            BindMappers();

            FormulaMapper mapper = Container.Resolve<FormulaMapper>();
            _taskViewFactory.Init(mapper);

            Container.BindInstance(_taskViewFactory);
            BindModel();

            Inventory inventory = Container.Resolve<Inventory>();
            FormulaTask formulaTask = Container.Resolve<FormulaTask>();

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
            
            Container.BindInstance(levelTasks);

            BindPresenters();
            BindView();
        }
    }
}