using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace DelimitedDataDeserializer
{
	public class FixedWidthDynamicDataReader
	{
		private readonly IEnumerable<Tuple<int, string, int>> _fields;

		public FixedWidthDynamicDataReader(IEnumerable<Tuple<int, string, int>> fields)
		{
			_fields = fields;
		}

		public dynamic ParseLine(string line)
		{
			dynamic parseLine = new ExpandoObject();
			int index = 0;
			
			foreach (var field in _fields.OrderBy(x => x.Item1))
			{
				string value = line.Substring(index, field.Item3);
				var p = parseLine as IDictionary<string, object>;
				p[field.Item2] = value.Trim();
				index = index + field.Item3;
			}

			return parseLine;
		}

		public string PrintHeader()
		{
			var stringBuilder = new StringBuilder();
			foreach (var field in _fields.OrderBy(x => x.Item1))
			{
				stringBuilder.Append(field.Item2.PadRight(field.Item3));
			}
			return stringBuilder.ToString();
		}
	}
}