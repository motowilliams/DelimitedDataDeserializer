using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Concrete
{
	public class when_inputs_contain_quoted_fields
	{
		[Fact]
		public void returns_field_with_quotes_removed()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImport>();
			string fileData = "\"FirstName\"	\"20\"	\"20.5\"	\"2011/09/01\"";
			var expected = new TestFileImport { Name = "FirstName", Age = 20, Wage = 20.5, EventDate = new DateTime(2011, 9, 01) };

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImport> actual = reader.ReadLine(fileData);

			//Assert
			Assert.Equal(expected, actual.Item2);
		}
	}
}