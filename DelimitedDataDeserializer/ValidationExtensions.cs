using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DelimitedDataDeserializer
{
	public static class ValidationExtensions
	{
		public static bool IsValid<T>(this Tuple<ICollection<ValidationResult>, T> source)
		{
			return source.Item1.Count() == 0;
		}

		public static bool IsValid<T>(this IEnumerable<Tuple<ICollection<ValidationResult>, T>> source)
		{
			return source.All(x => x.Item1.Count() == 0);
		}

		public static int ValidationResultCount<T>(this Tuple<ICollection<ValidationResult>, T> source)
		{
			return source.Item1.Count;
		}

		public static TList Append<TList, TValue>(this TList list, TValue item) where TList : class, ICollection<TValue>
		{
			if (list == null) throw new ArgumentNullException("list");
			list.Add(item);
			return list;
		}

		public static Tuple<ICollection<ValidationResult>, T> AddValidationResult<T>(this Tuple<ICollection<ValidationResult>, T> source, string format, params object[] args)
		{
			source.Item1.Add(new ValidationResult(string.Format(format, args)));
			return source;
		}

		public static Tuple<ICollection<ValidationResult>, T> AsValidatedResult<T>(this Tuple<ICollection<ValidationResult>, T> source)
		{
			ICollection<ValidationResult> validationResults = new Collection<ValidationResult>();
			ValidationContext validationContext = new ValidationContext(source.Item2, null, null);
			Validator.TryValidateObject(source.Item2, validationContext, validationResults, true);

			foreach (var validationResult in validationResults)
				source.Item1.Add(validationResult);

			return source;
		}
	}
}