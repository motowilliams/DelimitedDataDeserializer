using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Concrete
{
	public class when_inputs_are_valid_and_ouput_uses_data_annotations
	{
		[Fact]
		public void returns_validation_error_with_new_instance_for_single_data_field()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImport>();
			string fileData = "data";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImport> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.False(dataResult.IsValid());
		}

		[Fact]
		public void returns_validation_error_for_incorrect_attribute_ordering()
		{
			//Arrange
			var reader = new DelimitedDataReader<InvalidTestFileImport>();
			string fileData = "FirstName	20	20.5	2011/09/01";

			//Act
			Tuple<ICollection<ValidationResult>, InvalidTestFileImport> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.False(dataResult.IsValid());
		}

		[Fact]
		public void returns_zero_validation_errors_for_correct_file_data()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImport>();
			string fileData = "FirstName	20	20.5	2011/09/01";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImport> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.True(dataResult.IsValid());
		}

		[Fact]
		public void returns_one_validation_errors_for_mising_name()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithDataAnnotations>();
			string fileData = "	20	20.5	2011/09/01";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImportWithDataAnnotations> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.Equal(1, dataResult.ValidationResultCount());
		}

		[Fact]
		public void returns_two_validation_errors_for_mising_name_age()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithDataAnnotations>();
			string fileData = "		20.5	2011/09/01";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImportWithDataAnnotations> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.Equal(2, dataResult.ValidationResultCount());
		}

		[Fact]
		public void returns_three_validation_errors_for_mising_name_age_wage()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithDataAnnotations>();
			string fileData = "			2011/09/01";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImportWithDataAnnotations> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.Equal(3, dataResult.ValidationResultCount());
		}

	}
}