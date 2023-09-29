using Config.LevelTaskLogic;
using Config.ParametersLogic;
using Converter;
using Extensions;
using Model.TreeLogic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Model.LevelTaskLogic
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
            ParametersConfig parametersConfig,
            IConverter<string, Delegate> fromConfigString, 
            IConverter<BlockTree, Delegate> fromTree)
        {
            _treeToDelegate = fromTree;
            _tree = tree;
            _taskConfig = taskConfig;

            IEnumerable<bool> booleanDomain = new[]{false, true};
            _definitionArea = booleanDomain.InPower(parametersConfig.NumberOfParameters).Select(s => s.ToArray());

            _sourceFunction = fromConfigString.Convert(taskConfig.ParseString);
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