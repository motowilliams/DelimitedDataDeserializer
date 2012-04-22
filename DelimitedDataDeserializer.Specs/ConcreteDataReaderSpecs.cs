using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DelimitedDataDeserializer.Tests;
using Machine.Specifications;

namespace DelimitedDataDeserializer.Specs
{
    public class ConcreteDataReaderSpec
    {
        Establish context = () =>
            reader = new DelimitedDataReader<TestFileImport>();

        protected static DelimitedDataReader<TestFileImport> reader;
    }

    [Subject(typeof(DelimitedDataReader<TestFileImport>))]
    public class when_parsing_null_input : ConcreteDataReaderSpec
    {
        Because of = () =>
            readLine = reader.ReadLine(null);

        It should_return_invalid = () =>
            readLine.IsValid().ShouldBeFalse();

        protected static Tuple<ICollection<ValidationResult>, TestFileImport> readLine;
    }

    [Subject(typeof(DelimitedDataReader<TestFileImport>))]
    public class when_parsing_mismatched_input : ConcreteDataReaderSpec
    {
        Because of = () =>
            readLine = reader.ReadLine(" ");

        It should_return_invalid = () =>
            readLine.IsValid().ShouldBeFalse();

        protected static Tuple<ICollection<ValidationResult>, TestFileImport> readLine;
    }

    [Subject(typeof(DelimitedDataReader<TestFileImport>))]
    public class when_parsing_valid_input
    {
        Establish context = () =>
            reader = new DelimitedDataReader<SingleObject>();

        Because of = () =>
            readLine = reader.ReadLine(" ");

        It should_return_valid = () =>
            readLine.IsValid().ShouldBeTrue();

        protected static Tuple<ICollection<ValidationResult>, SingleObject> readLine;
        protected static DelimitedDataReader<SingleObject> reader;
    }
}
