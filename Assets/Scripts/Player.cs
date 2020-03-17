using System;
using UnityEngine;

namespace SFStudio.OpenWorld
{
	using Skill;

	public class Player : MonoBehaviour
	{
		[Header("Skill")] [SerializeField] ActiveSkill[] ActiveSkills = new ActiveSkill[0];

		void Update()
		{
			foreach (ActiveSkill skill in ActiveSkills)
			{
				if (skill == null) continue;

				foreach (string binding in skill.bindings)
				{
					if (Input.GetButtonDown(binding))
					{
						skill.OnButtonDown();
					}
					
					if (Input.GetButtonUp(binding))
					{
						skill.OnButtonUp();
					}
				}
			}
		}
	}
}