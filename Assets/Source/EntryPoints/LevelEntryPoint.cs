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
using Model.LevelStateLogic;

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
        [SerializeField] private FormulaTaskView _formulaTaskView;
        [SerializeField] private TreeExecutionButton _treeExecutionButton;
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
            Container.Bind<LevelState>().AsSingle();
        }

        private void BindPresenters()
        {
            Container.Bind<PlacingPresenter>().AsSingle();
            Container.Bind<RemovingPresenter>().AsSingle();
            Container.Bind<BuilderPresenter>().AsSingle();

            Container.Bind<PlayerFormulaPresenter>().AsSingle();
            Container.Bind<FinisherPresenter>().AsSingle();
        }

        private void BindView()
        {
            Container.BindInstance(_mapView).NonLazy();
            Container.BindInstance(_builderView).NonLazy();
            Container.BindInstance(_inventoryView).NonLazy();

            Container.BindInstance(_playerFormulaView).NonLazy();
            Container.BindInstance(_formulaTaskView).NonLazy();

            Container.BindInstance(_levelTasksView).NonLazy();

            Container.BindInstance(_removingButton).NonLazy();
            Container.BindInstance(_treeExecutionButton).NonLazy();
        }

        public override void InstallBindings()
        {
            BindConfigs();
            BindMappers();
            Container.BindInstance(_taskViewFactory);
            BindModel();

            FormulaTask formulaTask = Container.Resolve<FormulaTask>();

            var amountSaveTaskBuilder = new AmountSaveTaskBuilder();
            AmountSaveTask amountSaveTask2Stars = amountSaveTaskBuilder
                .StartBuilding()
                .RegisterOperation(LogicOperationType.NOT, 8)
                .RegisterOperation(LogicOperationType.OR, 4)
                .Build();

            AmountSaveTask amountSaveTask3Stars = amountSaveTaskBuilder
                .StartBuilding()
                .RegisterOperation(LogicOperationType.OR, 5)
                .Build();
            
            var levelTasksBuilder = new LevelTasksBuilder();
            LevelTasks levelTasks = levelTasksBuilder
                .StartBuilding()
                .RegisterForOneStar(formulaTask)
                .RegisterForTwoStars(amountSaveTask2Stars)
                .RegisterForTreeStars(amountSaveTask3Stars)
                .Build();
            
            Container.BindInstance(levelTasks);
            Container.Bind<ScoreCalculator>().AsSingle();

            BindPresenters();
            BindView();
        }
    }
}