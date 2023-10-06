using Configs;
using UnityEngine;
using Zenject;

namespace EntryPoints
{
    public class GameEntryPoint : MonoInstaller
    {
        [SerializeField] private FormulaConfig _formulaConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_formulaConfig);
            Container.BindInstance(_formulaConfig.ParameterNames);
        }
    }
}