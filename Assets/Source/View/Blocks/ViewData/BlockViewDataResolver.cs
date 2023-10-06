using System;
using Model.BlocksLogic.OperationBlocksLogic;
using UnityEngine;

namespace View.Blocks.ViewData
{
    [CreateAssetMenu(fileName = "Block View Data Resolver", menuName = "Data/Block View Data Resolver", order = 51)]
    public class BlockViewDataResolver : ScriptableObject
    {
        [SerializeField] private OperationViewData _operationNot;
        [SerializeField] private OperationViewData _operationOr;
        [SerializeField] private OperationViewData _operationAnd;
        [SerializeField] private OperationViewData _operationXor;
        [SerializeField] private OperationViewData _operationNor;
        [SerializeField] private ParameterViewData _parameter;

        public ParameterViewData ParameterData => _parameter;

        public OperationViewData GetOperationData(LogicOperationType operationType)
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