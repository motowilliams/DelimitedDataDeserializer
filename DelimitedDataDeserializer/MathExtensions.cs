using System;
using System.Collections.Generic;
using System.Linq;

namespace DelimitedDataDeserializer
{
	public static class MathExtensions
	{
		//http://stackoverflow.com/questions/1426715/factorial-of-n-numbers-using-c-lambda
		public static int Factorial(this int count)
		{
			return count == 0
					   ? 1
					   : Enumerable.Range(1, count).Aggregate((i, j) => i * j);
		}

		public static int Factorial(this IEnumerable<int> range)
		{
			return range.Aggregate((i, j) => i * j);
		}
	}
}