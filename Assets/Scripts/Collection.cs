using System;
using System.Collections.Generic;
using System.Linq;

namespace SFStudio.OpenWorld
{
	public class Collection<T> 
		where T : Item
	{
		public class Builder
		{
			string collectionName;
			int size;
			
			public Builder SetName(string collectionName)
			{
				this.collectionName = collectionName;
				return this;
			}

			public Builder SetSize(int size)
			{
				this.size = size;
				return this;
			}
			
			public Collection<T> Build()
			{
				return new Collection<T>(this.size)
				{
					collectionName = this.collectionName,
				};
			}
		}

		UnityIntIntEvent onItemAdd;
		UnityIntIntEvent onItemRemove;
		UnityIntIntEvent onIndexChanged;
		
		/// <summary>
		/// (old size, new size)
		/// </summary>
		UnityIntIntEvent onSizeChanged;

		public Collection(int size)
		{
			items = new T[size];
		}
		
		public string collectionName;
		protected T[] items = new T[0];

		public int Count
		{
			get { return items.Length; }
		}

		public T this[int index]
		{
			get { return items[index]; }
		}

		public bool CanAdd(T item, int amount)
		{
			throw new NotImplementedException();
		}

		public void Add(T item, int amount)
		{
			throw new NotImplementedException();
		}

		public bool CanRemove(T item, int amount)
		{
			throw new NotImplementedException();
		}

		public void Remove(T item, int amount)
		{
			throw new NotImplementedException();
		}

		public void Swap(int fromIndex, Collection<T> to, int toIndex)
		{
			throw new NotImplementedException();
		}

		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(Predicate<T> predicate)
		{
			throw new NotImplementedException();
		}

		public int GetCanAddAmount(T item)
		{
			throw new NotImplementedException();
		}

		public int GetAmount(int index)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public void Sort(IComparer<T> comparer)
		{
			throw new NotImplementedException();
		}
		
		#region Modify Collection

		public void Resize(int newSize)
		{
			if (newSize > Count)
			{
				Expand(newSize - Count);
			}
			else
			{
				Shrink(Count - newSize);
			}
		}

		public void Expand(int count)
		{
			int current = Count;
			Array.Resize(ref items, current + count);
			
			onSizeChanged.Invoke(current, current + count);
		}

		public void Shrink(int count)
		{
			int current = Count;
			Array.Resize(ref items, current - count);
			
			onSizeChanged.Invoke(current, current - count);
		}
		
		#endregion

		#region Utilities

		protected bool AreEqual(T a, T b)
		{
			return EqualityComparer<T>.Default.Equals(a, b);
		}

		protected bool IsNull(T item)
		{
			return item.Equals(default(T)) || item == null;
		}
		
		public T[] ToArray()
		{
			return items;
		}

		public List<T> ToList()
		{
			return items.ToList();
		}

		public override string ToString()
		{
			return collectionName;
		}
		
		#endregion
	}
}