using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace zintersid.Common.Transformers
{
    /// <summary>
    /// Transforms route tokens to kebab-case, but leaves route parameters (e.g., {userId}) unchanged.
    /// </summary>
    public class ToKebabParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null)
                return null;

            var str = value.ToString() ?? string.Empty;

            // Do not transform route parameters (e.g., tokens like "userId" in {userId})
            if (str.StartsWith("{") && str.EndsWith("}"))
                return str;

            // Transform to kebab-case
            return Regex.Replace(str, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}