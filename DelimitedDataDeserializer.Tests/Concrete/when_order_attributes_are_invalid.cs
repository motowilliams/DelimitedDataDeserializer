using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DelimitedDataDeserializer.Tests.Concrete
{
	public class when_order_attributes_are_invalid
	{
		[Fact]
		public void throw_when_destination_has_no_order_attributes()
		{
			//Arrange
			var reader = new DelimitedDataReader<TestFileImportWithNoAttributes>();
			string fileData = "FirstName	20	20.5	2011/09/01";

			//Act
			var exception = Assert.Throws<ValidationException>(() => reader.ReadLine(fileData));

			//Assert
			Assert.Contains("does not have Order attribute", exception.Message);
		}
	}
}