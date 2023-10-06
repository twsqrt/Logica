using Configs;
using Mappers;
using UnityEngine;
using Zenject;

namespace EntryPoints
{
    public class GameEntryPoint : MonoInstaller
    {
        [SerializeField] private FormulaConfig _formulaConfig;
        [SerializeField] private FormulaParser _formulaParser;

        public override void InstallBindings()
        {
            Container.BindInstance(_formulaConfig);
            Container.BindInstance(_formulaConfig.ParameterNames);
            Container.BindInstance(_formulaParser);
        }
    }
}