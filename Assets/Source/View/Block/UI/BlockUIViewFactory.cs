using UnityEngine;
using Model.BlockLogic.BlockDataLogic;
using System.Collections.Generic;
using Config;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    [CreateAssetMenu(fileName = "BlockUIViewFactory", menuName = "View/BlockUIViewFactory", order = 51)]
    public class BlockUIViewFactory : ScriptableObject, IBlockDataBasedFactory<BlockUIView>
    {
        [SerializeField] private OperationViewDataResolver _operationResolver;
        [SerializeField] private ParameterViewData _parameterViewData;
        [SerializeField] private OperationUIView _operationPrefab;
        [SerializeField] private ParameterUIView _parameterPrefab;

        private Dictionary<int, string> _parameterNames;

        public void Init(ParameterConfig config)
        {
            _parameterNames = config.ToDictionary();
        }

        public BlockUIView Create(OperationData data)
        {
            OperationUIView view = Instantiate(_operationPrefab);
            OperationViewData viewData = _operationResolver.Resolve(data.OperationType);
            view.Init(viewData);

            return view;
        }

        public BlockUIView Create(ParameterData data)
        {
            string parameterName = _parameterNames[data.Id];
            ParameterUIView view = Instantiate(_parameterPrefab);
            view.Init(_parameterViewData, parameterName);
            
            return view;
        }
    }
}