using System;

namespace DelimitedDataDeserializer
{
    public static class ReflectionExtensions
    {
        public static bool IsNullable(this Type source)
        {
            return source.IsGenericType && source.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNotNullable(this Type source)
        {
            return !source.IsNullable();
        }
    }
}