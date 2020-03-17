using UnityEngine;

namespace SFStudio.OpenWorld.Skill
{
	public abstract class SkillBase : ScriptableObject
	{
		public string skillName = default;
		
		public virtual void Execute()
		{
#if UNITY_EDITOR
			Debug.Log($"{skillName} Executed.");
#endif
		}
	}
}