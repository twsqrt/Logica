using Configs.LevelConfigs;
using Mappers;
using Model.BlockLogic;
using Model.BlocksLogic;
using Model.InventoryLogic;
using Model.LevelStateLogic;
using Model.LevelTasks;
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
using Zenject;

namespace EntryPoints
{
    public class LevelEntryPoint : MonoInstaller
    {
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
            //LevelConfig levelConfig = LevelConfigLoader.Load("xyz parameters");
            //LevelConfig levelConfig = LevelConfigLoader.Load("abcd parameters");
            //LevelConfig levelConfig = LevelConfigLoader.Load("Zhegalkin polynomials");
            LevelConfig levelConfig = LevelConfigLoader.Load("circuitry");

            Container.BindInstance(levelConfig);
            Container.BindInstance(levelConfig.Map);
            Container.BindInstance(levelConfig.Inventory);
            Container.BindInstance(levelConfig.Tree);
            Container.BindInstance(levelConfig.Tasks);
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
            Container.Bind<LevelState>().AsSingle();

            Container.Bind<LevelTaskFactory>().AsSingle();
            Container.Bind<LevelTasks>().AsSingle();
            Container.Bind<ScoreCalculator>().AsSingle();
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

            BindModel();
            BindPresenters();
            BindView();
        }
    }
}