using System;
using System.Collections.Generic;
using Boo.Lang;

namespace SFStudio.OpenWorld
{
	public class ItemSlot<T> where T : Item
	{
		public Collection<ItemSlot<T>, T> address;
		
		public int slotIndex;
		public int amount;

		T item;
		
		public bool Vacant
		{
			get { return item == null; }
		}

		public T Item
		{
			get { return item; }
			protected set { item = value; }
		}

		public bool CanRegister(int insertAmount)
		{
			return address.CanInsert(slotIndex, item, insertAmount);
		}

		public void Register(int insertAmount)
		{
			address.Insert(slotIndex, item, insertAmount);
		}

		public void RegisterSilent(T t, int insertAmount = 1)
		{
			if (!EqualityComparer<T>.Default.Equals(item, t))
			{
				ItemSlot<T> ownerSlot = item.ownerSlot as ItemSlot<T>;
				if (ownerSlot != null && ownerSlot == this)
				{
					t.ownerSlot = null;
				}
			}

			item = t;
			amount = insertAmount;
			if (Vacant)
			{
				amount = 0;
			}
			
			ItemSlot<T> ownerSlot2 = item.ownerSlot as ItemSlot<T>;
			if (ownerSlot2 != null && ownerSlot2 == this)
			{
				t.ownerSlot = this as ItemSlot<Item>;
			}
		}

		public void Clear()
		{
			var entry = item as ItemSlot<T>;
			if (entry != null && entry == this)
			{
				item.ownerSlot = null;
			}

			item = default(T);
			amount = 0;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public override string ToString()
		{
			return $"{item}:{amount:N0}";
		}
	}
}