using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Dynamic
{
	public class when_inputs_contain_quoted_fields : DynamicContext
	{
		private new readonly string[] _values = new[] { "\"Value01\"", "\"Value02\"", "\"Value03\"" };

		[Fact]
		public void returns_field_with_quotes_removed()
		{
			//Arrange
			var reader = new DynamicDelimitedDataReader(_fields);
			dynamic expected = InitializeDynamic(_fields, _values.Select(x => x.RemoveBoundingQuote()).ToArray());

			//Act
			dynamic actual = reader.ParseLine(_values.JoinWithTabs());

			//Assert
			// as IDictionary<string, object>
			var expectedValues = (expected as IDictionary<string, object>).Select(x => x.Value);
			var actualValues = (actual as IDictionary<string, object>).Select(x => x.Value);
			Assert.Equal(expectedValues, actualValues);
		}
	}
}