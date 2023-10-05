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

        public OperationViewData GetOperationData(OperationBlockType operationType)
            => operationType switch
        {
            OperationBlockType.NOT => _operationNot,
            OperationBlockType.OR => _operationOr,
            OperationBlockType.AND => _operationAnd,
            OperationBlockType.XOR => _operationXor,
            OperationBlockType.NOR => _operationNor,
            _ => throw new ArgumentException($"Operation Type {operationType} not found!"),
        };
    }
}