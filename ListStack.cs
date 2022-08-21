using System.Collections.Generic;

namespace OWRichPresence;

public class ListStack<T>
{
	private readonly List<T> _items = new();

	public void Clear()
		=> _items.Clear();

	public void Push(T item)
		=> _items.Add(item);

	public T Pop()
	{
		if (_items.Count > 0)
		{
			var temp = _items[_items.Count - 1];
			_items.RemoveAt(_items.Count - 1);
			return temp;
		}

		return default;
	}

	public T Peek() => _items.Count > 0
		? _items[_items.Count - 1]
		: default;

	public void RemoveAt(int index)
		=> _items.RemoveAt(index);

	public bool Remove(T item)
		=> _items.Remove(item);
}
