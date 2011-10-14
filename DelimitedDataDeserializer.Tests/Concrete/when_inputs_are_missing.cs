using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Concrete
{
	public class when_inputs_are_missing
	{
		[Fact]
		public void returns_line_null_validation_error_for_null_input()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImport>();
			string fileData = null;

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImport> readLine = reader.ReadLine(fileData);

			//Assert
			Assert.False(readLine.IsValid());
		}

		[Fact]
		public void returns_property_count_mismatch_validation_error_for_null_input()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImport>();
			string fileData = "	";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImport> readLine = reader.ReadLine(fileData);

			//Assert
			Assert.False(readLine.IsValid());
		}

		[Fact]
		public void returns_()
		{
			//Arrange
			var reader = new DelimitedDataReader<SingleObject>();
			string fileData = "	";

			//Act
			Tuple<ICollection<ValidationResult>, SingleObject> readLine = reader.ReadLine(fileData);

			//Assert
			Assert.False(readLine.IsValid());
		}
	}
}