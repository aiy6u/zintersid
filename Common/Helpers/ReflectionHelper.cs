using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace zintersid.Common.Helpers
{
    public static class ReflectionHelper
    {
        public static Dictionary<string, string> GetConstantsWithDescriptions(Type type)
        {
            var constantsWithDescriptions = new Dictionary<string, string>();

            // Get all public static fields of the specified type
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                             .Where(fi => fi.IsLiteral && !fi.IsInitOnly);

            foreach (var field in fields)
            {
                // Get the value of the constant
                var value = field.GetValue(null)?.ToString();

                // Get the description attribute, if it exists
                var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
                var description = descriptionAttribute != null ? descriptionAttribute.Description : string.Empty;

                if (value != null)
                {
                    constantsWithDescriptions[value] = description;
                }
            }

            return constantsWithDescriptions;
        }
    }
}