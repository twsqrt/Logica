using Configs.LevelConfigs.LevelTasksConfigs;
using Converter;
using Extensions;
using Model.TreeLogic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Model.LevelTasksLogic
{
    public class FormulaTask : ILevelTask
    {
        private readonly IConverter<BlockTree, Delegate> _treeToDelegate;
        private readonly BlockTree _tree;
        private readonly IEnumerable<IEnumerable<bool>> _definitionArea;
        private readonly Delegate _sourceFunction;
        private readonly FormulaTaskConfig _taskConfig;

        public FormulaTaskConfig TaskConfig => _taskConfig;

        public FormulaTask(BlockTree tree, 
            FormulaTaskConfig taskConfig, 
            IConverter<string, Delegate> fromConfigString, 
            IConverter<BlockTree, Delegate> fromTree)
        {
            _treeToDelegate = fromTree;
            _tree = tree;
            _taskConfig = taskConfig;

            IEnumerable<bool> booleanDomain = new[]{false, true};
            int numberOfParameters = taskConfig.ParametersId.Count();
            _definitionArea = booleanDomain.InPower(numberOfParameters).Select(s => s.ToArray());

            _sourceFunction = fromConfigString.Convert(taskConfig.ParseText);
        }

        public bool CheckCompletion()
        {
            Delegate playerFunction = _treeToDelegate.Convert(_tree);
            foreach(IEnumerable<bool> parameters in _definitionArea)
            {
                object[] dynamicInvokeParameters = parameters.Cast<object>().ToArray();
                bool playerFunctionResult = (bool) playerFunction.DynamicInvoke(dynamicInvokeParameters);
                bool sourceFunctionResult = (bool) _sourceFunction.DynamicInvoke(dynamicInvokeParameters);

                if(playerFunctionResult != sourceFunctionResult)
                    return false;
            }

            return true;
        }

        public T Accept<T>(ILevelTaskVisitor<T> visitor)
            => visitor.Visit(this);
    }
}