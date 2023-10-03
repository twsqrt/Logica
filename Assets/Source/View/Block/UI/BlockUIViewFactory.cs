using Config.LevelLogic.ParametersLogic;
using Model.BlockLogic.BlockDataLogic;
using System.Collections.Generic;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    [CreateAssetMenu(fileName = "Block UI View Factory", menuName = "Factory/Block UI View", order = 51)]
    public class BlockUIViewFactory : ScriptableObject, IBlockDataBasedFactory<BlockUIView>
    {
        [SerializeField] private BlockViewDataResolver _viewDataResolver;
        [SerializeField] private OperationUIView _operationPrefab;
        [SerializeField] private ParameterUIView _parameterPrefab;

        private Dictionary<int, string> _parameterNames;

        public void Init(ParametersConfig parametersConfig)
        {
            _parameterNames = parametersConfig.ToNameDictionary();
        }

        public BlockUIView Create(OperationData data)
        {
            OperationUIView view = Instantiate(_operationPrefab);
            OperationViewData viewData = _viewDataResolver.GetOperationData(data.OperationType);
            view.Init(viewData);

            return view;
        }

        public BlockUIView Create(ParameterData data)
        {
            string parameterName = _parameterNames[data.Id];
            ParameterUIView view = Instantiate(_parameterPrefab);
            view.Init(_viewDataResolver.ParameterData, parameterName);
            
            return view;
        }
    }
}