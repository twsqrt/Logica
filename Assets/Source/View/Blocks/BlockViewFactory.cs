using Configs;
using Model.BlocksLogic.OperationBlocksLogic;
using Model.BlocksLogic;
using UnityEngine;
using View.Blocks.ViewData;

namespace View.Blocks
{
    [CreateAssetMenu(fileName = "Block View Factory", menuName = "Factory/Block View", order = 51)]
    public class BlockViewFactory : ScriptableObject, IBlockVisitor<BlockView>
    {
        [SerializeField] private BlockViewDataResolver _viewDataResolver;
        [SerializeField] private OperationView _operationPrefab;
        [SerializeField] private ParameterView _parameterPrefab;
        [SerializeField] private ParameterNamesConfig _parametersConifg;


        private BlockView VisitOperation(OperationBlock operation)
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

        public BlockView Visit(ParameterBlock parameter)
        {
            ParameterView parameterView = Instantiate(_parameterPrefab);
            string name = _parametersConifg[parameter.Id];
            parameterView.Init(_viewDataResolver.ParameterData, name, parameter);

            return parameterView;
        }
    }
}