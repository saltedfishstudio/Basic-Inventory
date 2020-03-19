using System;

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
			
		}

		public void Clear()
		{
			throw new NotImplementedException();
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