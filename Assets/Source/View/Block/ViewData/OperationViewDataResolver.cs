using System;
using Model.BlockLogic.LogicOperationLogic;
using UnityEngine;

namespace View.BlockLogic.ViewDataLogic
{
    [CreateAssetMenu(fileName = "OperationViewDataResolver", menuName = "View/ViewData/OperationViewDataResolver", order = 51)]
    public class OperationViewDataResolver : ScriptableObject
    {
        [SerializeField] private OperationViewData _operationNot;
        [SerializeField] private OperationViewData _operationOr;
        [SerializeField] private OperationViewData _operationAnd;
        [SerializeField] private OperationViewData _operationXor;
        [SerializeField] private OperationViewData _operationNor;

        public OperationViewData Resolve(LogicOperationType operationType)
            => operationType switch
        {
            LogicOperationType.NOT => _operationNot,
            LogicOperationType.OR => _operationOr,
            LogicOperationType.AND => _operationAnd,
            LogicOperationType.XOR => _operationXor,
            LogicOperationType.NOR => _operationNor,
            _ => throw new ArgumentException($"Operation Type {operationType} not found!"),
        };
    }
}