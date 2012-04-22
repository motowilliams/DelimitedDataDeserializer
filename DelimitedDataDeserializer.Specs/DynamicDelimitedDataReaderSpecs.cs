using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using DelimitedDataDeserializer.Tests;
using Machine.Specifications;

namespace DelimitedDataDeserializer.Specs
{
    [Subject(typeof(DynamicDelimitedDataReader))]
    public class when_inputs_are_missing
    {
        Establish context = () =>
                            reader = new DynamicDelimitedDataReader(_fields);

        Because of = () =>
                     line = reader.ParseLine(null);

        It should_be_null = () => 
                            ShouldExtensionMethods.ShouldBeNull(line);

        protected static dynamic line;
        protected static string[] _fields = new[] { "Field01", "Field02", "Field03" };
        protected static DynamicDelimitedDataReader reader;
    }

    [Subject(typeof(DynamicDelimitedDataReader))]
    public class when_parsing_empty_input
    {
        Establish context = () =>
            reader = new DynamicDelimitedDataReader(_fields);

        Because of = () => 
            Exception = Catch.Exception(() => line = reader.ParseLine(" "));

        It should_fail =
            () => Exception.ShouldBeOfType(typeof (ApplicationException));

        protected static dynamic line;
        protected static DynamicDelimitedDataReader reader;
        protected static Exception Exception;
        protected static string[] _fields = new[] { "Field01", "Field02", "Field03" };
    }

    [Subject(typeof(DynamicDelimitedDataReader))]
    public class when_single_inputs_are_valid : DynamicDelimitedDataReaderSpec
    {
        Because of = () =>
            actual = reader.ParseLine(_values.JoinWithTabs());

        It should_return_valid_field = () =>
            ShouldExtensionMethods.ShouldEqual(actual, InitializeDynamic(_fields, _values));

        protected static dynamic actual;
    }

    [Subject(typeof(DynamicDelimitedDataReader))]
    public class when_multiple_inputs_are_valid : DynamicDelimitedDataReaderSpec
    {
        Because of = () =>
            actual = reader.ParseLines(new List<string> { _values.JoinWithTabs(), _values.JoinWithTabs() });

        It should_return_valid_fields = () => 
            ShouldExtensionMethods.ShouldEqual(actual, expected);

        protected static dynamic actual;
        protected static dynamic expectedItem = InitializeDynamic(_fields, _values);
        protected static List<dynamic> expected = new List<dynamic> { expectedItem, expectedItem };
    }

    public abstract class DynamicDelimitedDataReaderSpec
    {
        Establish context = () =>
            reader = new DynamicDelimitedDataReader(_fields);

        protected static DynamicDelimitedDataReader reader;

        protected static readonly string[] _fields = new[] { "Field01", "Field02", "Field03" };
        protected static readonly string[] _values = new[] { "Value01", "Value02", "Value03" };

        protected static dynamic InitializeDynamic(string[] fields, string[] values)
        {
            dynamic parseLine = new ExpandoObject();
            for (var i = 0; i < fields.Count(); i++)
            {
                var p = parseLine as IDictionary<string, object>;
                p[_fields[i]] = values[i];
            }
            return parseLine;
        }
    }
}
