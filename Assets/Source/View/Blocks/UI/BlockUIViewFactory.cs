using Model.BlocksLogic.BlocksData;
using Configs;
using UnityEngine;
using View.Blocks.ViewData;

namespace View.Blocks.UI
{
    [CreateAssetMenu(fileName = "Block UI View Factory", menuName = "Factory/Block UI View", order = 51)]
    public class BlockUIViewFactory : ScriptableObject, IBlockDataBasedFactory<BlockUIView>
    {
        [SerializeField] private BlockViewDataResolver _viewDataResolver;
        [SerializeField] private OperationUIView _operationPrefab;
        [SerializeField] private ParameterUIView _parameterPrefab;
        [SerializeField] private ParameterNamesConfig _parametersConfig;

        public BlockUIView Create(OperationData data)
        {
            OperationUIView view = Instantiate(_operationPrefab);
            OperationViewData viewData = _viewDataResolver.GetOperationData(data.OperationType);
            view.Init(viewData);

            return view;
        }

        public BlockUIView Create(ParameterData data)
        {
            string parameterName = _parametersConfig[data.Id];
            ParameterUIView view = Instantiate(_parameterPrefab);
            view.Init(_viewDataResolver.ParameterData, parameterName);
            
            return view;
        }
    }
}