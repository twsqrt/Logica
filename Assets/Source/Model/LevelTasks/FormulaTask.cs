using Configs.LevelConfigs.LevelTasksConfigs;
using Extensions;
using Mappers;
using Model.TreeLogic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Model.LevelTasksLogic
{
    public class FormulaTask : ILevelTask
    {
        private readonly DelegateMapper _delegateMaper;
        private readonly BlockTree _tree;
        private readonly IEnumerable<IEnumerable<bool>> _definitionArea;
        private readonly TruthTable _sourceTruthTable;
        private readonly FormulaTaskConfig _taskConfig;

        public FormulaTaskConfig TaskConfig => _taskConfig;

        public FormulaTask(BlockTree tree, FormulaTaskConfig taskConfig, DelegateMapper delegateMapper)
        {
            _tree = tree;
            _delegateMaper = delegateMapper;
            _sourceTruthTable = taskConfig.TruthTable;
            _taskConfig = taskConfig;

            IEnumerable<bool> booleanDomain = new[]{false, true};
            int numberOfParameters = taskConfig.ParametersId.Count();
            _definitionArea = booleanDomain.InPower(numberOfParameters).Select(s => s.ToArray());

        }

        public bool CheckCompletion()
        {
            Delegate playerFunction = _delegateMaper.From(_tree);
            foreach(IEnumerable<bool> parameters in _definitionArea)
            {
                object[] dynamicInvokeParameters = parameters.Cast<object>().ToArray();
                bool playerFunctionResult = (bool) playerFunction.DynamicInvoke(dynamicInvokeParameters);

                if(playerFunctionResult != _sourceTruthTable[parameters])
                    return false;
            }

            return true;
        }

        public T Accept<T>(ILevelTaskVisitor<T> visitor)
            => visitor.Visit(this);
    }
}