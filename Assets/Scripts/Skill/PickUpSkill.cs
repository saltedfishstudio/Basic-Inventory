using UnityEngine;

namespace SFStudio.OpenWorld.Skill
{
	[CreateAssetMenu(menuName = "Skill/PickUp Skill", order = 403)]
	public class PickUpSkill : ActiveSkill
	{
		ItemFactory targetFactory = default;
		Inventory inventory = default;
		
		const int amount = -1;

		protected override bool CanExecute()
		{
			return targetFactory != null;
		}

		protected override void DoExecute()
		{
			base.DoExecute();
			targetFactory.TryPickUpItem(inventory, amount);
		}

		public override void Load(Player newPlayer)
		{
			base.Load(newPlayer);
			
			EventDispatcher.Register<ItemFactory>("OnPickUpReady", OnPickUpReady);
			EventDispatcher.Register("OnPickUpDeprecated", OnPickUpDeprecated);
		}

		public override void Unload()
		{
			EventDispatcher.Unregister<ItemFactory>("OnPickUpReady", OnPickUpReady);
			EventDispatcher.Unregister("OnPickUpDeprecated", OnPickUpDeprecated);
		}

		void OnPickUpReady(ItemFactory factory)
		{
			this.targetFactory = factory;
			this.inventory = player.inventory;
		}

		void OnPickUpDeprecated()
		{
			this.targetFactory = null;
			this.inventory = null;
		}
	}
}