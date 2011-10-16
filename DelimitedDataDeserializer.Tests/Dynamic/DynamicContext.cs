using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace DelimitedDataDeserializer.Tests.Dynamic
{
	public abstract class DynamicContext
	{
		protected readonly string[] _fields = new[] { "Field01", "Field02", "Field03" };
		protected readonly string[] _values = new[] { "Value01", "Value02", "Value03" };

		protected dynamic InitializeDynamic(string[] fields, string[] values)
		{
			dynamic parseLine = new ExpandoObject();
			for (var i = 0; i < fields.Count(); i++)
			{
				var p = parseLine as IDictionary<string, object>;
				p[_fields[i]] = values[i];
			}
			return parseLine;
		}
	}
}