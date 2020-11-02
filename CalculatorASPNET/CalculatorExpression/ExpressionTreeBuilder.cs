using System;
using System.Linq;
using System.Linq.Expressions;

namespace CalculatorExpression
{
	public static class ExpressionTreeBuilder
	{
		private static readonly char[] HighPriorityOperations = {'*', '/'};
		private static readonly char[] LowPriorityOperations = {'+', '-'};
		
		public static Expression Build(string str)
		{
			#region Parsing To Linked List
			var firstNode = new LinkedListNode();//Dummy node
			var lastNode = firstNode;
			for (var i = 0; i < str.Length; i++)
			{
				if (char.IsDigit(str[i]) || i == 0 && str[i] == '-')
				{
					var numberEnd = GetNumberEnd(str, i);
					lastNode.Next = new LinkedListNode
					{
						Expression = Expression.Constant(double.Parse(str.Substring(i, numberEnd - i + 1)))
					};
					lastNode.Next.Previous = lastNode;
					lastNode = lastNode.Next;
					i = numberEnd;
					continue;
				}
				if (HighPriorityOperations.Contains(str[i]) || LowPriorityOperations.Contains(str[i]))
				{
					lastNode.Next = new LinkedListNode
					{
						Operator = str[i]
					};
					lastNode.Next.Previous = lastNode;
					lastNode = lastNode.Next;
					continue;
				}
				if (str[i] == '(')
				{
					var end = GetParenthesedExpressionEnd(str, i);
					lastNode.Next = new LinkedListNode
					{
						Expression = Build(str.Substring(i + 1, end - i))
					};
					lastNode.Next.Previous = lastNode;
					lastNode = lastNode.Next;
					i = end;
				}
			}
			#endregion
			//Handling dummy node
			if (firstNode.Next == null)
				return null;
			firstNode = firstNode.Next;
			firstNode.Previous = null;
			while (firstNode.Next != null) //Only one node will be left
			{
				var current = firstNode.Next;
				while (current != null)//Only operator nodes matter, so penultimate node is the last interesting one
				{
					if (!(LowPriorityOperations.Contains(current.Operator) && !AreOnlyLowPrioritiesAround(current)))
					{
						var newNode = new LinkedListNode
						{
							Expression = ExpressionFromOperatorNode(current)
						};
						newNode.Previous = current.Previous.Previous;
						newNode.Next = current.Next.Next;
						if (current.Previous.Previous == null)
							firstNode = newNode;
						else
							current.Previous.Previous.Next = newNode;
						if (current.Next.Next != null)
							current.Next.Next.Previous = newNode;
					}
					current = current.Next.Next;
				}
			}
			return firstNode.Expression;
		}

		private static int GetNumberEnd(string str, int numberStart)
		{
			for (var i = numberStart + 1; i < str.Length; i++)
				if (!char.IsDigit(str[i]) && str[i] != '.')
					return i - 1;
			return str.Length - 1;
		}
		
		private static int GetParenthesedExpressionEnd(string str, int start)
		{
			var counter = 1;
			for (var i = start + 1; i < str.Length; i++)
			{
				if (str[i] == ')')
				{
					counter--;
					if (counter == 0)
						return i;
				}
				if (str[i] == '(')
					counter++;
			}
			throw new ArgumentException($"Syntax error: parentheses at {start} is not closed");
		}

		private static bool AreOnlyLowPrioritiesAround(LinkedListNode node)
		{
			return (node.Previous.Previous == null || LowPriorityOperations.Contains(node.Previous.Previous.Operator))
			       && (node.Next.Next == null || LowPriorityOperations.Contains(node.Next.Next.Operator));
		}

		private static Expression ExpressionFromOperatorNode(LinkedListNode current) =>
			current.Operator switch
			{
				'+' => Expression.Add(current.Previous.Expression, current.Next.Expression),
				'-' => Expression.Subtract(current.Previous.Expression, current.Next.Expression),
				'*' => Expression.Multiply(current.Previous.Expression, current.Next.Expression),
				'/' => Expression.Divide(current.Previous.Expression, current.Next.Expression),
				_ => throw new ArgumentException("Syntax error: invalid operator")
			};
	}
}