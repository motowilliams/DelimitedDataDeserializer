using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DelimitedDataDeserializer
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class OrderAttribute : Attribute
	{
		public int Order { get; private set; }

		public OrderAttribute(int i)
		{
			Order = i;
		}

		public static int GetOrderAttributeValue(PropertyInfo propertyInfo)
		{
			var orderAttribute = propertyInfo.GetCustomAttributes(typeof(OrderAttribute), true).Cast<OrderAttribute>().FirstOrDefault();

			if (orderAttribute == null)
				throw new ValidationException(StringExtensions.Format("Destination property ({0}) does not have Order attribute", propertyInfo.Name));

			return orderAttribute.Order;
		}
	}
}