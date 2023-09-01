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

        private BlockView VisitOperation(LogicOperation operation)
        {
            OperationViewData viewData = _operationResolver.Resolve(operation.OperationType);
            OperationView view = Instantiate(_operationPrefab);
            view.Init(viewData, operation);

            return view;
        }

        public BlockView Visit(OperationNot operationNot)
            => VisitOperation(operationNot);

        public BlockView Visit(BinaryOperaion binaryOperaion)
            => VisitOperation(binaryOperaion);

        public BlockView Visit(Parameter parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}