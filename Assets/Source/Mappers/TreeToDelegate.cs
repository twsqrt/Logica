using Model.BlocksLogic.OperationBlocksLogic;
using Model.BlocksLogic;
using Model.TreeLogic;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Configs.LevelConfigs.LevelTasksConfigs;

namespace Mappers
{
    public class TreeToDelegate
    {
        private class Visitor : IBlockVisitor<Expression>
        {
            private readonly Dictionary<int, ParameterExpression> _parameters;

            public IEnumerable<ParameterExpression> Parameters => _parameters.Values;

            private Expression CreateNorExpression(Expression leftOperand, Expression rightOperand)
            {
                Expression or = Expression.OrElse(leftOperand, rightOperand);
                return Expression.Not(or);
            }

            public Visitor(FormulaTaskConfig formulaTaskConfig)
            {
                _parameters = formulaTaskConfig.ParametersId
                    .ToDictionary(i => i, i => Expression.Parameter(typeof(bool), $"id{i}"));
            }

            public Expression Visit(IReadOnlyOperationNot operationNot)
            {
                Expression operandExpression = operationNot.Operand.Accept(this);
                return Expression.Not(operandExpression);
            }

            public Expression Visit(IReadOnlyBinaryOperation binaryOperation)
            {
                Expression left = binaryOperation.FirstOperand.Accept(this);
                Expression right = binaryOperation.SecondOperand.Accept(this);

                LogicOperationType binaryOperationType = binaryOperation.OperationType;
                return binaryOperationType switch
                {
                    LogicOperationType.OR => Expression.OrElse(left, right),
                    LogicOperationType.AND => Expression.AndAlso(left, right),
                    LogicOperationType.XOR => Expression.ExclusiveOr(left, right),
                    LogicOperationType.NOR => CreateNorExpression(left, right),
                    _ => throw new ArgumentException($"Binary operation {binaryOperationType} not found!"),
                };
            }

            public Expression Visit(IReadOnlyParameterBlock parameter)
                => _parameters[parameter.Id];
        }

        private readonly Visitor _visitor;

        public TreeToDelegate(FormulaTaskConfig formulaTaskConfig)
        {
            _visitor = new Visitor(formulaTaskConfig);
        }

        public Delegate Convert(BlockTree tree)
        {
            Expression body = tree.CurrentRoot.Accept(_visitor);
            LambdaExpression lambdaExpression = Expression.Lambda(body, _visitor.Parameters);
            return lambdaExpression.Compile();
        }
    }
}