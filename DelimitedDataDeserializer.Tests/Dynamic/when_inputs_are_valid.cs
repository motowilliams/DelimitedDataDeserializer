using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Dynamic
{
	public class when_inputs_are_valid
	{
		readonly string[] _fields = new[] { "Field01", "Field02", "Field03" };
		readonly string[] _values = new[] { "Value01", "Value02", "Value03" };

		[Fact]
		public void returns_valid_dynamic_for_single_valid_input()
		{
			//Arrange
			var reader = new DynamicDelimitedDataReader(_fields);
			dynamic expected = InitializeDynamic(_fields, _values);

			//Act
			dynamic actual = reader.ParseLine(_values.JoinWithTabs());

			//Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void returns_valid_dynamic_for_multiple_valid_input()
		{
			//Arrange
			var reader = new DynamicDelimitedDataReader(_fields);
			dynamic expectedItem = InitializeDynamic(_fields, _values);
			List<dynamic> expected = new List<dynamic> { expectedItem, expectedItem };
			var list = new List<string> { _values.JoinWithTabs(), _values.JoinWithTabs() };

			//Act
			dynamic actual = reader.ParseLines(list);

			//Assert
			Assert.Equal(expected, actual);
		}

		private dynamic InitializeDynamic(string[] fields, string[] values)
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