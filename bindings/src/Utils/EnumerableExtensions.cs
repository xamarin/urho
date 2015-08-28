using System.Collections.Generic;

namespace Urho
{
	public static class EnumerableExtensions
	{
		public static void AddToValueList<TKey, TItem>(this Dictionary<TKey, List<TItem>> source, TKey key, TItem item)
		{
			List<TItem> items;
			if (source.TryGetValue(key, out items))
			{
				items.Add(item);
			}
			else
			{
				items = new List<TItem> { item };
				source[key] = items;
			}
		}

		public static void RemoveFromValueList<TKey, TItem>(this Dictionary<TKey, List<TItem>> source, TKey key, TItem item)
		{
			List<TItem> items;
			if (source.TryGetValue(key, out items))
			{
				items.Remove(item);
				if (items.Count == 0)
				{
					source.Remove(key);
				}
			}
		}

		public static void RemoveFromAllValueLists<TKey, TItem>(this Dictionary<TKey, List<TItem>> source, TItem item)
		{
			foreach (var keyValue in source)
			{
				keyValue.Value.Remove(item);
			}
		}
	}
}
