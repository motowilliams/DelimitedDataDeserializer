using System;
using System.ComponentModel.DataAnnotations;

namespace DelimitedDataDeserializer.Tests
{
	public class TestFileImportWithNullableTypesDataAnnotations
	{
		/// <summary>
		/// [Order(1)]
		/// </summary>
		[Order(1)]
		[Range(1, 50)]
		public int Age { get; set; }

		/// <summary>
		/// [Order(2)]
		/// </summary>
		[Order(2)]
		[Range(1.0d, 20.75d)]
		public double? Wage { get; set; }

		/// <summary>
		/// [Order(3)]
		/// </summary>
		[Order(3)]
		[Range(typeof(DateTime), "2011/08/01", "2011/09/05", ErrorMessage = "Value for {0} must be between {1} and {2}")]
		public DateTime? EventDate { get; set; }
	}
}