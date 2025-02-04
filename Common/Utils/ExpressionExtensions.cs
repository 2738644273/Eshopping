﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Utils
{
    //Expressing扩展类
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first, Expression<Func<T, bool>> secend)
        {
            return first.AndAlso(secend, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first, Expression<Func<T, bool>> secend)
        {
            return first.AndAlso(secend, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2,
            Func<Expression, Expression, BinaryExpression> func)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(func(left, right), parameter);
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;

                return base.Visit(node);
            }
        }
    }
}
