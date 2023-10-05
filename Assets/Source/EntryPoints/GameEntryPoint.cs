using Configs;
using UnityEngine;
using Zenject;

namespace EntryPoints
{
    public class GameEntryPoint : MonoInstaller
    {
        [SerializeField] private ParameterNamesConfig _parameterNamesConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_parameterNamesConfig);
        }
    }
}