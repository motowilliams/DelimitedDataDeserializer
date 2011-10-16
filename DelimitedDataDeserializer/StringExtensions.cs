using System;
using System.Linq;

namespace DelimitedDataDeserializer
{
	public static class StringExtensions
	{
		public static string FormatWith(this string format, params object[] args)
		{
			return String.Format(format, args);
		}

		public static string RemoveBoundingQuote(this string lineDataItem)
		{
			if (String.IsNullOrWhiteSpace(lineDataItem))
				return lineDataItem;

			const char _quoteChar = '\"';

			if (lineDataItem.First() == _quoteChar)
				lineDataItem = lineDataItem.Substring(1);

			if (lineDataItem.Last() == _quoteChar)
				lineDataItem = lineDataItem.Substring(0, lineDataItem.Length - 1);
			
			return lineDataItem;
		}
	}
}