using UnityEngine;

namespace SFStudio.OpenWorld.Skill
{
	public abstract class SkillBase : ScriptableObject
	{
		public string skillName = default;
		protected Player player;
		
		public virtual void Execute()
		{
			if (CanExecute())
			{
#if UNITY_EDITOR
			Debug.Log($"{skillName} Executed.");
#endif
				
				DoExecute();
			}
		}

		protected virtual bool CanExecute()
		{
			return true;
		}

		protected virtual void DoExecute()
		{
			
		}

		public virtual void Load(Player newPlayer)
		{
			this.player = newPlayer;
		}


		public virtual void Unload()
		{
			
		}
	}
}