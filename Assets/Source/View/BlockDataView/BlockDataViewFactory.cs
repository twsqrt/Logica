using UnityEngine;
using Model.BlockLogic.BlockDataLogic;
using Model.BlockLogic.LogicOperationLogic;
using System;
using System.Collections.Generic;
using Config;
using config;

namespace View.BlockDataLogic
{
    [CreateAssetMenu(fileName = "BlockDataViewFactory", menuName = "View/BlockDataViewFactory", order = 51)]
    public class BlockDataViewFactory : ScriptableObject, IBlockDataBasedFactory<BlockDataView>
    {
        [SerializeField] private BlockDataView _operationNot;
        [SerializeField] private BlockDataView _operationOr;
        [SerializeField] private BlockDataView _operationAnd;
        [SerializeField] private BlockDataView _operationXor;
        [SerializeField] private BlockDataView _operationNor;
        [SerializeField] private ParameterView _parameter;

        private Dictionary<int, string> _parameterNames;

        public void Init(ParameterConfig config)
        {
            _parameterNames = config.ToDictionary();
        }

        private BlockDataView GetOperationPrefabByType(LogicOperationType type) 
            => type switch
        {
            LogicOperationType.NOT => _operationNot,
            LogicOperationType.OR => _operationOr,
            LogicOperationType.AND => _operationAnd,
            LogicOperationType.XOR => _operationXor,
            LogicOperationType.NOR => _operationNor,
            _ => throw new ArgumentException($"Operation type: {type} not found!"),
        };

        public BlockDataView Create(OperationData data)
        {
            BlockDataView view = Instantiate(GetOperationPrefabByType(data.Type));
            view.Init();
            return view;
        }

        public BlockDataView Create(ParameterData data)
        {
            string name = _parameterNames[data.Id];

            ParameterView view = Instantiate(_parameter);
            view.Init(name);
            return view;
        }
    }
}