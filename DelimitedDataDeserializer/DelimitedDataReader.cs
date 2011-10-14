using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DelimitedDataDeserializer
{
	public class DelimitedDataReader<T> where T : new()
	{
		private readonly Type _type;
		private readonly PropertyInfo[] _propertyInfoCollection;
		private readonly char _delimeter;
		private IOrderedEnumerable<AnnotatedProperties> _annotatedProperties;

		public DelimitedDataReader(char delimeter = '\t')
		{
			_delimeter = delimeter;
			_type = typeof(T);
			_propertyInfoCollection = _type.GetProperties();
		}

		public Tuple<ICollection<ValidationResult>, T> ReadLine(string lineData)
		{
			if (lineData == null)
				return LineDataResult("lineData was null");

			string[] lineDataItems = lineData.Split(_delimeter);

			_annotatedProperties = _propertyInfoCollection
				.Select(x => new AnnotatedProperties { Name = x.Name, Order = OrderAttribute.GetOrderAttributeValue(x), PropertyInfo = x })
				.OrderBy(x => x.Order);

			//if the number of annotated properties doesn't match the number of fields in the line then return validation errors
			if (_annotatedProperties.Count() != lineDataItems.Count())
				return LineDataResult("lineData has {0} fields, {1} has {2}", lineDataItems.Count(), _type.Name, _annotatedProperties.Count());

			// Validate the target class has the correct [Order(x)] attribute value per the properties it contains
			if (_annotatedProperties.Count().Factorial() != _annotatedProperties.Select(x => x.Order).Factorial())
				return LineDataResult("Order attribute sequence problem on {0}", _type.Name);

			T readLine = ReadSplitLine(lineDataItems);

			ICollection<ValidationResult> results = new Collection<ValidationResult>();
			Tuple<ICollection<ValidationResult>, T> fileDataResult = Tuple.Create(results, readLine);

			return fileDataResult.AsValidatedResult();
		}

		public T ReadSplitLine(string[] splitLine)
		{
			T item = new T();

			foreach (var annotatedProperty in _annotatedProperties)
			{
				Type propertyType = annotatedProperty.PropertyInfo.PropertyType;

				dynamic value = splitLine[annotatedProperty.Order - 1];
				dynamic baseTypeValue;

				if (propertyType.IsNotNullable())
				{
					baseTypeValue = GetBaseTypeValue(propertyType, value, propertyType.IsNullable());
				}
				else
				{
					//Unwrap the Nullable generic to the basetype and set property to that
					Type baseType = propertyType.GetGenericArguments()[0];
					baseTypeValue = GetBaseTypeValue(baseType, value, propertyType.IsNullable());
				}
				annotatedProperty.PropertyInfo.SetValue(item, baseTypeValue, null);
			}
			return item;
		}

		private Tuple<ICollection<ValidationResult>, T> LineDataResult(string format, params object[] args)
		{
			ICollection<ValidationResult> validationResults = new List<ValidationResult> { new ValidationResult(string.Format(format, args)) };
			Tuple<ICollection<ValidationResult>, T> readLine = Tuple.Create(validationResults, new T());
			return readLine;
		}

		private Tuple<ICollection<ValidationResult>, T> LineDataResult(string errorMessage)
		{
			ICollection<ValidationResult> validationResults = new List<ValidationResult> { new ValidationResult(errorMessage) };
			Tuple<ICollection<ValidationResult>, T> readLine = Tuple.Create(validationResults, new T());
			return readLine;
		}

		private class AnnotatedProperties
		{
			public string Name { get; set; }
			public int Order { get; set; }
			public PropertyInfo PropertyInfo { get; set; }
		}

		private dynamic GetBaseTypeValue(Type type, dynamic value, bool nullable)
		{
			MethodInfo parseMethod = type.GetMethod("TryParse", new Type[] { typeof(string), type.MakeByRefType() });

			if (parseMethod == null)
				return value;

			var parameters = new object[] { value, null };
			var success = (bool)parseMethod.Invoke(null, parameters);

			// if TryParse returns true(success) return the value from the out parameter
			if (success)
				return parameters[1];

			// if TryParse returns false(success)
			if (nullable)
				return null;

			return parameters[1];
		}
	}
}