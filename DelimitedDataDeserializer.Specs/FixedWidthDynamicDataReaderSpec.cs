using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using DelimitedDataDeserializer.Tests;
using Machine.Specifications;

namespace DelimitedDataDeserializer.Specs
{
    public class FixedWidthDynamicDataReaderSpec
    {
        protected static IEnumerable<Tuple<int, string, int>> fixedWidthFields =
            new List<Tuple<int, string, int>>()
                {
                    Tuple.Create(1, "Field01", 10),
                    Tuple.Create(2, "Field02", 5),
                    Tuple.Create(3, "Field03", 20)
                };

        Establish context = () => 
            reader = new FixedWidthDynamicDataReader(fixedWidthFields);

        protected static FixedWidthDynamicDataReader reader;
    }

    [Subject(typeof(FixedWidthDynamicDataReader))]
    public class when_parsing_a_fixed_width_line : FixedWidthDynamicDataReaderSpec
    {
        Because of = () => 
            actual = reader.ParseLine("FirstName 25   300 Smithsonian Lane");

        It should_parsed_field01 = () =>
            ShouldExtensionMethods.ShouldEqual(actual.Field01, "FirstName");

        It should_parsed_field02 = () =>
            ShouldExtensionMethods.ShouldEqual(actual.Field02, "25");

        It should_parsed_field03 = () =>
            ShouldExtensionMethods.ShouldEqual(actual.Field03, "300 Smithsonian Lane");

        protected static dynamic actual;
    }

    public class when_printing_a_fixed_width_header : FixedWidthDynamicDataReaderSpec
    {
        Because of = () =>
            actual = reader.PrintHeader();

        It should_return_headher = () =>
            actual.ShouldEqual("Field01   Field02Field03             ");

        protected static string actual;
    }

    public class when_validating_null_input 
    {
        Establish context = () =>
            reader = new FixedWidthDataReader<TestFixedWidthFileImport>();

        Because of = () =>
            actual = reader.ParseLine(null);

        It should_return_as_invalid = () => 
            actual.IsValid().ShouldBeFalse();

        protected static Tuple<ICollection<ValidationResult>, TestFixedWidthFileImport> actual; 
        protected static FixedWidthDataReader<TestFixedWidthFileImport> reader;
    }

    public class when_validating_existing_but_valid_input
    {
        Establish context = () =>
            reader = new FixedWidthDataReader<TestFixedWidthFileImport>();

        Because of = () =>
            actual = reader.ParseLine("John Smith          2012.312011-01-01 23:12:55.999");

        It should_return_as_valid = () =>
            actual.IsValid().ShouldBeTrue();

        protected static Tuple<ICollection<ValidationResult>, TestFixedWidthFileImport> actual;
        protected static FixedWidthDataReader<TestFixedWidthFileImport> reader;
    }

}
