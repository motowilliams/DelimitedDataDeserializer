using System;
using System.Collections.Generic;

namespace DelimitedDataDeserializer.Tests
{
	public static class StringExtensions
	{
		public static string JoinWithTabs(this IEnumerable<string> source )
		{
			return string.Join("~", source).Replace('~','\t');
		}
	}
}