using System;
using Xunit;

namespace DelimitedDataDeserializer.Tests.FixedWidth
{
	public class when_inputs_are_valid_for_concrete
	{
		[Fact]
		public void returns_validation_error_for_null_input()
		{
			//Arrange
			var reader = new FixedWidthDataReader<TestFixedWidthFileImport>();
			var expected = new TestFixedWidthFileImport { Age = 20, EventDate = new DateTime(2011, 1, 1, 23, 12, 55, 999), Name = "John Smith", Wage = 12.31 };

			//Act
			var actual = reader.ParseLine(null);

			//Assert
			Assert.False(actual.IsValid());
		}

		[Fact]
		public void returns_validation_error_for_invald_input()
		{
			//Arrange
			var reader = new FixedWidthDataReader<TestFixedWidthFileImport>();
			var expected = new TestFixedWidthFileImport { Age = 20, EventDate = new DateTime(2011, 1, 1, 23, 12, 55, 999), Name = "John Smith", Wage = 12.31 };

			//Act
			var actual = reader.ParseLine("                    2012.312011-01-01 23:12:55.999");

			//Assert
			Assert.False(actual.IsValid());
		}

		[Fact]
		public void returns_valid_concrete_for_valid_input()
		{
			//Arrange
			var reader = new FixedWidthDataReader<TestFixedWidthFileImport>();
			var expected = new TestFixedWidthFileImport { Age = 20, EventDate = new DateTime(2011, 1, 1, 23, 12, 55, 999), Name = "John Smith", Wage = 12.31 };

			//Act
			TestFixedWidthFileImport actual = reader.ParseLine("John Smith          2012.312011-01-01 23:12:55.999").Item2;

			//Assert
			Assert.Equal(expected, actual);
		}
	}
}
 