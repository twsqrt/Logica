using System.Collections.Generic;
using Config;
using Model.BlockLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using UnityEngine;
using View.BlockLogic.ViewDataLogic;

namespace View.BlockLogic
{
    [CreateAssetMenu(fileName = "BlockViewFactory", menuName = "View/BlockViewFactory", order = 51)]
    public class BlockViewFactory : ScriptableObject, IBlockVisitor<BlockView>
    {
        [SerializeField] private OperationViewDataResolver _operationResolver;
        [SerializeField] private OperationView _operationPrefab;
        [SerializeField] private ParameterViewData _parameterViewData;
        [SerializeField] private ParameterView _parameterPrefab;

        private Dictionary<int, string> _parameterNames;

        public void Init(ParameterBlocksConfig config)
        {
            _parameterNames = config.GetParameterNameByIdDictionary();
        }

        private BlockView VisitOperation(LogicOperation operation)
        {
            OperationView view = Instantiate(_operationPrefab);
            OperationViewData viewData = _operationResolver.Resolve(operation.OperationType);
            view.Init(viewData, operation);

            return view;
        }

        public BlockView Visit(OperationNot operationNot)
            => VisitOperation(operationNot);

        public BlockView Visit(BinaryOperaion binaryOperaion)
            => VisitOperation(binaryOperaion);

        public BlockView Visit(Parameter parameter)
        {
            ParameterView parameterView = Instantiate(_parameterPrefab);
            string name = _parameterNames[parameter.Id];
            parameterView.Init(_parameterViewData, name, parameter);

            return parameterView;
        }
    }
}