using System;
using System.Linq;

namespace DelimitedDataDeserializer
{
	public static class StringExtensions
	{
		private const char QuoteChar = '\"';

		public static string Format(this string format, params object[] args)
		{
			return String.Format(format, args);
		}

		public static string RemoveBoundingQuote(this string lineDataItem)
		{
			if (String.IsNullOrWhiteSpace(lineDataItem))
				return lineDataItem;

			if (lineDataItem.First() == QuoteChar)
				lineDataItem = lineDataItem.Substring(1);

			if (lineDataItem.Last() == QuoteChar)
				lineDataItem = lineDataItem.Substring(0, lineDataItem.Length - 1);
			
			return lineDataItem;
		}
	}
}