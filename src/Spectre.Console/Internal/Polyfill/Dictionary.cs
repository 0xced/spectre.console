#if NETSTANDARD2_0
namespace System.Collections.Generic;

internal static class DictionaryExtensions
{
    /// <summary>
    /// Attempts to add the specified key and value to the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add. It can be <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the key/value pair was added to the dictionary successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Unlike the <see cref="IDictionary{TKey,TValue}.Add(TKey,TValue)"/> method, this method doesn't throw an exception if the element with the given key exists in the dictionary.
    /// Unlike the Dictionary indexer, <c>TryAdd</c> doesn't override the element if the element with the given key exists in the dictionary.
    /// If the key already exists, <c>TryAdd</c> does nothing and returns <see langword="false"/>.
    /// </remarks>
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            return false;
        }

        dictionary[key] = value;
        return true;
    }
}
#endif