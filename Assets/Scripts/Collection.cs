using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SFStudio.OpenWorld
{
	public class Collection<TSlot, TItem> 
		where TSlot : ItemSlot<TItem>
		where TItem : Item
	{

		Dictionary<string, int> dict;
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
			
			public Collection<TSlot, TItem> Build()
			{
				return new Collection<TSlot,TItem>(this.size)
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
			items = new TSlot[size];
		}
		
		public string collectionName;
		protected TSlot[] items;

		public int Count
		{
			get { return items.Length; }
		}

		public TItem this[int index]
		{
			get { return items[index].Item; }
		}

		public bool CanAdd(TItem item, int amount)
		{
			int canAddAmount = GetCanAddAmount(item, amount);
			
			if (canAddAmount < amount) 
				return false;

			return true;
		}

		public bool Add(TItem item, int amount)
		{
			if (!CanAdd(item, amount))
			{
				return false;
			}

			int totalAddAmount = amount;
			var affected = new List<(int, int)>();

			bool Execute(int index)
			{
				// 비어있지 않거나 널인 경우 스킵
				if (!items[index].Vacant && IsNull(items[index].Item))
				{
					return false;
				}

				bool isEmpty = items[index] == null;
				int canAddToStackAmount = Mathf.Min(item.itemDefinition.maxStack - GetAmount(index), totalAddAmount);
				
				totalAddAmount -= canAddToStackAmount;
				affected.Add((index, canAddToStackAmount));

				if (totalAddAmount > 0)
				{
					if (isEmpty)
					{
						InsertInternal(index, item, GetAmount(index) + canAddToStackAmount);
					}
					else
					{
						InsertInternal(index, this[index], GetAmount(index) + canAddToStackAmount);
					}
				}
				else
				{
					InsertInternal(index, item, GetAmount(index) + canAddToStackAmount);
				}

				if (totalAddAmount <= 0)
				{
					return true;
				}

				if (isEmpty)
				{
					item = CreateElementClone(item);
				}

				return false;
			}

			// Stacking 시도
			for (int i = 0; i < items.Length; i++)
			{
				if (AreEqual(items[i].Item, item))
				{
					int currentAmount = GetAmount(i);
					
					int canAddAmount = Mathf.Min(item.itemDefinition.maxStack - currentAmount, amount);
					if (canAddAmount > 0 && 
					    CanInsert(i, item, currentAmount + canAddAmount))
					{
						// Do
						bool breaker = Execute(i);
						if (breaker) break;
					}
				}
			}

			// 빈 슬롯
			for (int i = 0; i < items.Length; i++)
			{
				int canAddAmount = Mathf.Min(item.itemDefinition.maxStack, amount);
				if (items[i].Vacant && 
				    CanInsert(i, item, canAddAmount))
				{
					// Do
					bool breaker = Execute(i);
					if (breaker) break;
				}
			}
			
			

			return true;
		}

		TItem CreateElementClone(TItem item)
		{
			throw new NotImplementedException();
		}

		public bool CanRemove(TItem item, int amount)
		{
			throw new NotImplementedException();
		}

		public void Remove(TItem item, int amount)
		{
			throw new NotImplementedException();
		}

		public bool CanInsert(int index, TItem item, int amount = 1)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, TItem item, int amount = 1)
		{
			throw new NotImplementedException();
		}
		
		private void InsertInternal(int index, TItem item, int amount)
		{
			// 새 데이터가 null 이거나 개수가 0인 경우
			// ok -> 해당 슬롯을 비웁니다.
			// no -> 해당 슬롯을 세팅합니다.
			if (IsNull(item) || amount <= 0)
			{
				items[index].Clear();
			}
			else
			{
				items[index].RegisterSilent(item, amount);
			}
		}

		public void Swap(int fromIndex, Collection<TSlot, TItem> to, int toIndex)
		{
			throw new NotImplementedException();
		}

		public bool Contains(TItem item)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(TItem item)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(Predicate<TItem> predicate)
		{
			throw new NotImplementedException();
		}

		public int GetCanAddAmount(TItem item, int escapeLimit)
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

		public void Sort(IComparer<TItem> comparer)
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

		protected bool AreEqual(TItem a, TItem b)
		{
			return EqualityComparer<TItem>.Default.Equals(a, b);
		}

		protected bool IsNull(TItem item)
		{
			return item.Equals(default(TItem)) || item == null;
		}
		
		public TSlot[] ToArray()
		{
			return items;
		}
		
		
		public List<TSlot> ToList()
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