using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DelimitedDataDeserializer
{
	public class FixedWidthDataReader<T> where T : new()
	{
		private readonly Type _type;
		private readonly PropertyInfo[] _propertyInfoCollection;
		private IOrderedEnumerable<AnnotatedProperties> _annotatedProperties;

		public FixedWidthDataReader()
		{
			_type = typeof(T);
			_propertyInfoCollection = _type.GetProperties();
		}

		public Tuple<ICollection<ValidationResult>, T> ParseLine(string lineData)
		{
			if (lineData == null)
				return LineDataResult("lineData was null");

			_annotatedProperties = _propertyInfoCollection
				.Select( x => new AnnotatedProperties { Name = x.Name, Length = FixedWidthAttribute.GetLengthAttributeValue(x), Position = FixedWidthAttribute.GetPositionAttributeValue(x), PropertyInfo = x })
				.OrderBy(x => x.Position);

			T item = new T();
			int index = 0;

			foreach (var annotatedProperty in _annotatedProperties)
			{
				Type propertyType = annotatedProperty.PropertyInfo.PropertyType;

				dynamic value = lineData.Substring(index, annotatedProperty.Length);
				dynamic baseTypeValue;
				index = index + annotatedProperty.Length;
				
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

			ICollection<ValidationResult> results = new Collection<ValidationResult>();
			Tuple<ICollection<ValidationResult>, T> fileDataResult = Tuple.Create(results, item);

			return fileDataResult.AsValidatedResult();
		}

		private dynamic GetBaseTypeValue(Type type, dynamic value, bool nullable)
		{
			MethodInfo parseMethod = type.GetMethod("TryParse", new Type[] { typeof(string), type.MakeByRefType() });

			if (parseMethod == null)
				return value.ToString().Trim();

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

		private class AnnotatedProperties
		{
			public string Name { get; set; }
			public int Position { get; set; }
			public int Length { get; set; }
			public PropertyInfo PropertyInfo { get; set; }
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

	}
}