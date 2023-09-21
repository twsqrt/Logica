using Model.MapLogic;
using System.Collections.Generic;
using System.Linq.Expressions;
using Extensions;
using Config;
using Model.TreeLogic;
using System.Linq;
using System;
using UnityEngine;
using Model.BlockLogic;

namespace Model
{
    public class FormulaRule
    {
        private readonly TreeToExpressionConverter _treeConverter;
        private readonly IEnumerable<IEnumerable<bool>> _definitionArea;

        //template
        private readonly Func<bool, bool, bool, bool> _sourceFunction;

        public FormulaRule(FormulaRuleConfig ruleConfig, ParametersConfig parametersConfig, TreeToExpressionConverter treeConverter)
        {
            _treeConverter = treeConverter;

            IEnumerable<bool> booleanDomain = new[]{false, true};
            _definitionArea = booleanDomain.InPower(parametersConfig.NumberOfParameters).Select(s => s.ToArray());

            //template
            _sourceFunction = (bool id1, bool id2, bool id3) => !(id1 && id3) || id2;
        }

        public bool Execute(Map map)
        {
            Block root = map[map.RootPosition].Block;

            Expression body = root.Accept(_treeConverter);
            LambdaExpression lambdaExpression = Expression.Lambda(body, _treeConverter.Parameters);
            Delegate playerFunction = lambdaExpression.Compile();

            Debug.Log(lambdaExpression);
            Debug.Log(_sourceFunction);

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