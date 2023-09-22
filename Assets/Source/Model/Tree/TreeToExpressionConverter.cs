using Config;
using Model.BlockLogic.LogicOperationLogic.BinaryOperationLogic;
using Model.BlockLogic.LogicOperationLogic;
using Model.BlockLogic;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Model.TreeLogic
{
    public class TreeToExpressionConverter : IBlockVisitor<Expression>
    {
        private readonly Dictionary<int, ParameterExpression> _parameters;

        public IEnumerable<ParameterExpression> Parameters => _parameters.Values;

        private Expression CreateNorExpression(Expression leftOperand, Expression rightOperand)
        {
            Expression or = Expression.OrElse(leftOperand, rightOperand);
            return Expression.Not(or);
        }

        public TreeToExpressionConverter(ParametersConfig config)
        {
            _parameters = config.GetParametersId()
                .ToDictionary(i => i, i => Expression.Parameter(typeof(bool), $"id{i}"));
        }

        public Expression Visit(OperationNot operationNot)
        {
            Expression operandExpression = operationNot.Operand.Accept(this);
            return Expression.Not(operandExpression);
        }

        public Expression Visit(BinaryOperation binaryOperation)
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

        public Expression Visit(Parameter parameter)
            => _parameters[parameter.Id];
    }
}