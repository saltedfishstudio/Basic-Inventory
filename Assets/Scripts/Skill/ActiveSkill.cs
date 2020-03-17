using UnityEngine;

namespace SFStudio.OpenWorld.Skill
{
	[CreateAssetMenu(menuName = "Skill/Active Skill", order = 402)]
	public class ActiveSkill : SkillBase
	{
		public string[] bindings = new string[0];

		public void OnButtonDown()
		{
			Execute();
		}

		public void OnButtonUp()
		{
			
		}

	}
}