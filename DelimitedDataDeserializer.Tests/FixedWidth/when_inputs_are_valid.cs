using System;
using System.Collections.Generic;
using System.Dynamic;
using Xunit;

namespace DelimitedDataDeserializer.Tests.FixedWidth
{
	public class when_inputs_are_valid 
	{
		[Fact]
		public void returns_valid_dynamic_for_single_valid_input()
		{
			//Arrange
			var fixedWidthFields = new List<Tuple<int, string, int>>();
			fixedWidthFields.Add(Tuple.Create(1, "Field01", 10));
			fixedWidthFields.Add(Tuple.Create(2, "Field02", 5));
			fixedWidthFields.Add(Tuple.Create(3, "Field03", 20));

			var reader = new FixedWidthDataReader(fixedWidthFields);
			dynamic expected = new ExpandoObject();
			expected.Field01 = "FirstName";
			expected.Field02 = "25";
			expected.Field03 = "300 Smithsonian Lane";

			//Act
			dynamic actual = reader.ParseLine("FirstName 25   300 Smithsonian Lane");

			//Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void returns_fixed_width_header()
		{
			//Arrange
			var fixedWidthFields = new List<Tuple<int, string, int>>();
			fixedWidthFields.Add(Tuple.Create(1, "Field01", 10));
			fixedWidthFields.Add(Tuple.Create(2, "Field02", 7));
			fixedWidthFields.Add(Tuple.Create(3, "Field03", 20));
			string expected = "Field01   Field02Field03             ";

			var reader = new FixedWidthDataReader(fixedWidthFields);
			
			//Act
			string actual = reader.PrintHeader();

			//Assert
			Assert.Equal(expected, actual);
		}
	}
}
