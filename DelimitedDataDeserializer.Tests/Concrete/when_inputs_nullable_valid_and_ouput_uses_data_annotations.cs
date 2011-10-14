using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Concrete
{
	public class when_inputs_nullable_valid_and_ouput_uses_data_annotations
	{
		[Fact]
		public void returns_validation_error_with_new_instance_for_single_data_field()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithNullableTypesDataAnnotations>();
			string fileData = "data";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImportWithNullableTypesDataAnnotations> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.False(dataResult.IsValid());
		}
		
		[Fact]
		public void returns_zero_validation_errors_for_correct_file_data()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithNullableTypesDataAnnotations>();
			string fileData = "20	20.5	2011/09/01";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImportWithNullableTypesDataAnnotations> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.True(dataResult.IsValid());
		}

		[Fact]
		public void returns_one_validation_errors_for_incorrect_type()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithNullableTypesDataAnnotations>();
			string fileData = "foo	20.5	2011/09/01";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImportWithNullableTypesDataAnnotations> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.Equal(1, dataResult.ValidationResultCount());
		}

		[Fact]
		public void returns_two_validation_errors_for_mising_name_age()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithNullableTypesDataAnnotations>();
			string fileData = "		";

			//Act
			Tuple<ICollection<ValidationResult>, TestFileImportWithNullableTypesDataAnnotations> dataResult = reader.ReadLine(fileData);

			//Assert
			Assert.Equal(1, dataResult.ValidationResultCount());
		}
	}
}