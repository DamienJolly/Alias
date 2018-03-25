using System;
using System.Collections.Concurrent;

namespace Alias.Emulator.Utilities
{
	sealed class ObjectPool<T>
	{
		private readonly ConcurrentBag<T> _objects;
		private readonly Func<T> _objectGenerator;

		public ObjectPool(Func<T> objectGenerator)
		{
			_objects = new ConcurrentBag<T>();
			_objectGenerator = objectGenerator ?? throw new ArgumentNullException("objectGenerator");
		}

		public T GetObject()
		{
			if (_objects.TryTake(out T Item))
			{
				return Item;
			}
			return _objectGenerator();
		}

		public void PutObject(T Item)
		{
			_objects.Add(Item);
		}
	}
}
