using System;

namespace DelimitedDataDeserializer.Tests
{
	public class TestFileImport
	{
		/// <summary>
		/// [Order(1)]
		/// </summary>
		[Order(1)]
		public string Name { get; set; }

		/// <summary>
		/// [Order(2)]
		/// </summary>
		[Order(2)]
		public int Age { get; set; }

		/// <summary>
		/// [Order(3)]
		/// </summary>
		[Order(3)]
		public double Wage { get; set; }

		/// <summary>
		/// [Order(4)]
		/// </summary>
		[Order(4)]
		public DateTime EventDate { get; set; }

		public override string ToString()
		{
			return string.Format("Name: {0}, Age: {1}, Wage: {2}, EventDate: {3}", Name, Age, Wage, EventDate);
		}

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