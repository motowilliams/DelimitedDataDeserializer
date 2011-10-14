using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace DelimitedDataDeserializer
{
	public class DynamicDelimitedDataReader
	{
		private readonly char _delimeter;
		private readonly string[] _fields;

		public DynamicDelimitedDataReader(string[] fields, char delimeter = '\t')
		{
			_fields = fields;
			_delimeter = delimeter;
		}

		public IEnumerable<dynamic> ParseLines(IEnumerable<string> fileData)
		{
			var objects = new List<dynamic>();

			foreach (var line in fileData)
			{
				dynamic o = ParseLine(line);
				objects.Add(o);
			}

			return objects;
		}

		public dynamic ParseLine(string line)
		{
			if (line == null)
				return null;

			var splitLine = line.Split(_delimeter);

			if (splitLine.Count() != _fields.Count())
				throw new ApplicationException("Invalid field count per configuration, expecting {0} fields".FormatWith(_fields.Count()));

			dynamic parseLine = new ExpandoObject();

			for (var i = 0; i < _fields.Count(); i++)
			{
				var p = parseLine as IDictionary<string, object>;
				p[_fields[i]] = splitLine[i];
			}

			return parseLine;
		}
	}
}