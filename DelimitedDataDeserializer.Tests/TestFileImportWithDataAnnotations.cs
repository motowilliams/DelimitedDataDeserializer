using System;
using System.ComponentModel.DataAnnotations;

namespace DelimitedDataDeserializer.Tests
{
	public class TestFileImportWithDataAnnotations
	{
		/// <summary>
		/// [Order(1)]
		/// </summary>
		[Order(1)]
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// [Order(2)]
		/// </summary>
		[Order(2)]
		[Range(1, 50)]
		public int Age { get; set; }

		/// <summary>
		/// [Order(3)]
		/// </summary>
		[Order(3)]
		[Range(1.0d, 20.75d)]
		public double Wage { get; set; }

		/// <summary>
		/// [Order(4)]
		/// </summary>
		[Order(4)]
		[Range(typeof(DateTime), "2011/08/01", "2011/09/05", ErrorMessage = "Value for {0} must be between {1} and {2}")]
		public DateTime EventDate { get; set; }

		public bool Equals(TestFileImport other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Name, Name) && other.Age == Age && other.Wage.Equals(Wage) && other.EventDate.Equals(EventDate);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(TestFileImport)) return false;
			return Equals((TestFileImport)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = (Name != null ? Name.GetHashCode() : 0);
				result = (result * 397) ^ Age;
				result = (result * 397) ^ Wage.GetHashCode();
				result = (result * 397) ^ EventDate.GetHashCode();
				return result;
			}
		}
	}
}