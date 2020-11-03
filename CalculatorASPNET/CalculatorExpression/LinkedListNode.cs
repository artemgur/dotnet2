using System.Linq.Expressions;

namespace CalculatorExpression
{
	public class LinkedListNode
	{
		public LinkedListNode Previous;
		public LinkedListNode Next;
		public Expression Expression;
		public char Operator;
	}
}