using System;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Dynamic
{
	public class when_inputs_are_missing
	{
		readonly string[] _fields = new[] { "Field01", "Field02", "Field03" };

		[Fact]
		public void returns_null_for_null_input()
		{
			//Arrange
			var reader = new DynamicDelimitedDataReader(_fields);

			//Act
			dynamic line = reader.ParseLine(null);

			//Assert
			Assert.Null(line);
		}

		[Fact]
		public void throws_for_empty_input()
		{
			//Arrange
			var reader = new DynamicDelimitedDataReader(_fields);
			string fileData = "	";

			//Act
			var exception = Assert.Throws<ApplicationException>(() => reader.ParseLine(fileData));

			//Assert
			Assert.Contains("invalid field count", exception.Message, StringComparison.OrdinalIgnoreCase);
		}
	}
}