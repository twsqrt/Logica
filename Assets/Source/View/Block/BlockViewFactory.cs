using Config.LevelLogic.ParametersLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using System.Collections.Generic;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;
using Config.LevelLogic.JsonConverter;

namespace View.BlockLogic
{
    [CreateAssetMenu(fileName = "Block View Factory", menuName = "Factory/Block View", order = 51)]
    public class BlockViewFactory : ScriptableObject, IBlockVisitor<BlockView>
    {
        [SerializeField] private BlockViewDataResolver _viewDataResolver;
        [SerializeField] private OperationView _operationPrefab;
        [SerializeField] private ParameterView _parameterPrefab;

        private Dictionary<int, string> _parameterNames;

        public void Init(ParametersConfig parametersConfig)
        {
            _parameterNames = parametersConfig.ToNameDictionary();
        }

        private BlockView VisitOperation(LogicOperation operation)
        {
            OperationView view = Instantiate(_operationPrefab);
            OperationViewData viewData = _viewDataResolver.GetOperationData(operation.OperationType);
            view.Init(viewData, operation);

            return view;
        }

        public BlockView Visit(OperationNot operationNot)
            => VisitOperation(operationNot);

        public BlockView Visit(BinaryOperation binaryOperation)
            => VisitOperation(binaryOperation);

        public BlockView Visit(Parameter parameter)
        {
            ParameterView parameterView = Instantiate(_parameterPrefab);
            string name = _parameterNames[parameter.Id];
            parameterView.Init(_viewDataResolver.ParameterData, name, parameter);

            return parameterView;
        }
    }
}