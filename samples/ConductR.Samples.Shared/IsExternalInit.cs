using System.ComponentModel;

namespace System.Runtime.CompilerServices;

/// <summary>
/// Ugly ass workaround in order to use immutable record goodness in netstandard2.1 libs.
/// <see href="https://github.com/dotnet/roslyn/issues/45510">Relevant Github issue</see>
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IsExternalInit { }
