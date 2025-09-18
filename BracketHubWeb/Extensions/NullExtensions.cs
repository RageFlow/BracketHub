using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BracketHubWeb.Extensions
{
    public static class NullExtensions
    {
        public static bool IsNotNull([NotNullWhen(true)] this object? o)
        {
            if (o == null)
                return false;

            return true;
        }

        public static void ThrowIfNull([NotNull] this object? o, string text)
        {
            if (o is null)
            {
                Throw(text);
            }
        }
        public static void ThrowIfNull([NotNull] this object? o, [CallerMemberName] string? caller = null, [CallerFilePath] string? path = null, [CallerLineNumber] int? line = null) => ThrowIfNull(o, $"Missing {caller} in {path} at line: {line}");

        [DoesNotReturn]
        public static void Throw(string? message) => throw new ArgumentNullException(message);
    }
}
