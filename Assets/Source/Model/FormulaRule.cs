using Config;
using Converter;
using Extensions;
using Model.TreeLogic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Model
{
    public class FormulaRule
    {
        private readonly IConverter<BlockTree, Delegate> _treeToDelegate;
        private readonly IEnumerable<IEnumerable<bool>> _definitionArea;
        private readonly Delegate _sourceFunction;

        public FormulaRule(FormulaRuleConfig ruleConfig, 
            ParametersConfig parametersConfig,
            IConverter<string, Delegate> fromConfigString, 
            IConverter<BlockTree, Delegate> fromTree)
        {
            _treeToDelegate = fromTree;

            IEnumerable<bool> booleanDomain = new[]{false, true};
            _definitionArea = booleanDomain.InPower(parametersConfig.NumberOfParameters).Select(s => s.ToArray());

            _sourceFunction = fromConfigString.Convert(ruleConfig.ParseString);
        }

        public bool Execute(BlockTree tree)
        {
            Delegate playerFunction = _treeToDelegate.Convert(tree);
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
    }
}