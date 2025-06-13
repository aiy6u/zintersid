using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace zintersid.Configurations
{
    public class KebabCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.ToDictionary(
                path => ConvertPathToKebabCase(path.Key),
                path => path.Value
            );

            swaggerDoc.Paths.Clear();
            foreach (var path in paths)
            {
                swaggerDoc.Paths.Add(path.Key, path.Value);
            }
        }

        // Only convert path segments, not route parameters (e.g., {userId})
        private string ConvertPathToKebabCase(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            var segments = path.Split('/');
            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                // Detect and preserve route parameters, e.g. {userId} or {userId:int}
                if (segment.StartsWith("{") && segment.EndsWith("}"))
                {
                    // Extract parameter name (may have colon for constraint)
                    var paramContent = segment.Substring(1, segment.Length - 2);
                    var colonIndex = paramContent.IndexOf(':');
                    if (colonIndex > 0)
                    {
                        // {userId:int} => {userId:int}
                        var paramName = paramContent.Substring(0, colonIndex);
                        var paramConstraint = paramContent.Substring(colonIndex + 1);
                        // Only kebab-case the constraint if needed, not the param name
                        segments[i] = "{" + paramName + ":" + paramConstraint + "}";
                    }
                    // else: {userId} => {userId}
                    continue;
                }
                // Convert to kebab-case
                segments[i] = ToKebabCase(segment);
            }
            return string.Join("/", segments);
        }

        private static string ToKebabCase(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            // Replace all non-parameter segments to kebab-case
            // Handles multiple uppercase letters (e.g., APIKey -> api-key)
            var kebab = Regex.Replace(value, "([a-z0-9])([A-Z])", "$1-$2");
            kebab = Regex.Replace(kebab, "([A-Z])([A-Z][a-z])", "$1-$2");
            return kebab.ToLower();
        }
    }
}