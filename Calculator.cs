using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Util
{
	public static class Calculator<T> where T : unmanaged
	{
		public delegate bool MathFuncCmp(T a, T b);
		public delegate T MathFuncPlural(T a, T b);
		public delegate T MathFuncSingle(T a);

		public static MathFuncPlural Add { get; } = CreateExpression<MathFuncPlural>(Expression.Add);
		public static MathFuncPlural Subtract { get; } = CreateExpression<MathFuncPlural>(Expression.Subtract);
		public static MathFuncPlural Multiply { get; } = CreateExpression<MathFuncPlural>(Expression.Multiply);
		public static MathFuncPlural Divide { get; } = CreateExpression<MathFuncPlural>(Expression.Divide);

		public static MathFuncSingle Negate { get; } = CreateExpression(Expression.Negate);
		public static MathFuncSingle Increment { get; } = CreateExpression(Expression.Increment);
		public static MathFuncSingle Decrement { get; } = CreateExpression(Expression.Decrement);

		public static MathFuncCmp Greater { get; } = CreateExpression<MathFuncCmp>(Expression.GreaterThan);
		public static MathFuncCmp GreaterEqual { get; } = CreateExpression<MathFuncCmp>(Expression.GreaterThanOrEqual);
		public static MathFuncCmp Less { get; } = CreateExpression<MathFuncCmp>(Expression.LessThan);
		public static MathFuncCmp LessEqual { get; } = CreateExpression<MathFuncCmp>(Expression.LessThanOrEqual);
		public static MathFuncCmp Equal { get; } = CreateExpression<MathFuncCmp>(Expression.Equal);
		public static MathFuncCmp NotEqual { get; } = CreateExpression<MathFuncCmp>(Expression.NotEqual);

		private static MF CreateExpression<MF>(Func<Expression, Expression, Expression> exprFunc)
		{
			var paramA = Expression.Parameter(typeof(T));
			var paramB = Expression.Parameter(typeof(T));
			var body = exprFunc(paramA, paramB);
			return Expression.Lambda<MF>(body, paramA, paramB).Compile();
		}

		private static MathFuncSingle CreateExpression(Func<Expression, Expression> exprFunc)
		{
			var paramA = Expression.Parameter(typeof(T));
			var body = exprFunc(paramA);
			return Expression.Lambda<MathFuncSingle>(body, paramA).Compile();
		}
	}

	public static class Calculator<T, K> where T : unmanaged
	{
		public static Func<T, K> Convert { get; } = CreateExpression(Expression.Convert);

		private static Func<T, K> CreateExpression(Func<Expression, Type, Expression> exprFunc)
		{
			var paramA = Expression.Parameter(typeof(T));
			var body = exprFunc(paramA, typeof(K));
			return Expression.Lambda<Func<T, K>>(body, paramA).Compile();
		}
	}

}