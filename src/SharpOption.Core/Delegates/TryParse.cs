using System.Net;

namespace SharpOption.Core.Delegates;

/// <summary>
/// A try parse function.
/// <para />
/// for example:
/// <list type="bullet">
/// <item><see cref="int.TryParse(string?, out int)"/>,</item>
/// <item><see cref="DateTime.TryParse(string?, out DateTime)"/>,</item>
/// <item><see cref="IPAddress.TryParse(string?, out IPAddress)"/>,</item>
/// <item>and many more.</item>
/// </list>
/// </summary>
public delegate bool TryParse<T>(string? s, out T value);

/// <summary>
/// A try parse function.
/// <para />
/// for example:
/// <list type="bullet">
/// <item><see cref="int.TryParse(ReadOnlySpan{char}, out int)"/>,</item>
/// <item><see cref="DateTime.TryParse(ReadOnlySpan{char}, out DateTime)"/>,</item>
/// <item><see cref="IPAddress.TryParse(ReadOnlySpan{char}, out IPAddress)"/>,</item>
/// <item>and many more.</item>
/// </list>
/// </summary>
public delegate bool TryParseSpan<T>(ReadOnlySpan<char> s, out T value);
