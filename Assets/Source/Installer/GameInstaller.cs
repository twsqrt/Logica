using Config;
using UnityEngine;
using Zenject;

namespace InstallerLogic
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ParameterNamesConfig _parameterNamesConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_parameterNamesConfig);
        }
    }
}