using System;
using System.ComponentModel.DataAnnotations;

namespace DelimitedDataDeserializer.Tests
{
	public class TestFixedWidthFileImport
	{
		[FixedWidth(1, 20)]
		[Required]
		public string Name { get; set; }

		[FixedWidth(2, 2)]
		public int Age { get; set; }

		[FixedWidth(3, 5)]
		public double Wage { get; set; }

		[FixedWidth(4, 23)]
		public DateTime EventDate { get; set; }

		public override string ToString()
		{
			return string.Format("Name: {0}, Age: {1}, Wage: {2}, EventDate: {3}", Name, Age, Wage, EventDate);
		}

		public bool Equals(TestFixedWidthFileImport other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Name, Name) && other.Age == Age && other.Wage.Equals(Wage) && other.EventDate.Equals(EventDate);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(TestFixedWidthFileImport)) return false;
			return Equals((TestFixedWidthFileImport)obj);
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
