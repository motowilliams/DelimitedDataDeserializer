using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DelimitedDataDeserializer
{
	public class FixedWidthAttribute : Attribute
	{
		public readonly int Position;
		public readonly int Length;

		public FixedWidthAttribute(int position, int length)
		{
			Position = position;
			Length = length;
		}

		public static int GetPositionAttributeValue(PropertyInfo propertyInfo)
		{
			var fixedWidthAttribute = propertyInfo.GetCustomAttributes(typeof(FixedWidthAttribute), true).Cast<FixedWidthAttribute>().FirstOrDefault();

			if (fixedWidthAttribute == null)
				throw new ValidationException(StringExtensions.Format("Destination property ({0}) does not have Order attribute", propertyInfo.Name));

			return fixedWidthAttribute.Position;
		}

		public static int GetLengthAttributeValue(PropertyInfo propertyInfo)
		{
			var fixedWidthAttribute = propertyInfo.GetCustomAttributes(typeof(FixedWidthAttribute), true).Cast<FixedWidthAttribute>().FirstOrDefault();

			if (fixedWidthAttribute == null)
				throw new ValidationException(StringExtensions.Format("Destination property ({0}) does not have Order attribute", propertyInfo.Name));

			return fixedWidthAttribute.Length;
		}
	}
}