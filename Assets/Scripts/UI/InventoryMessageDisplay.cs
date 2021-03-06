﻿using TMPro;
using UnityEngine;

namespace SFStudio.OpenWorld.UI
{
	public class InventoryMessageDisplay : MonoBehaviour
	{
		public TextMeshProUGUI messageBox;

		void Awake()
		{
			Initialize();
		}

		void OnEnable()
		{
			EventDispatcher.Register<string>("OnItemFactoryTriggerEnter", OnItemFactoryTriggerEnter);
			EventDispatcher.Register("OnItemFactoryTriggerExit", OnItemFactoryTriggerExit);
		}

		void OnDisable()
		{
			EventDispatcher.Unregister<string>("OnItemFactoryTriggerEnter", OnItemFactoryTriggerEnter);
			EventDispatcher.Unregister("OnItemFactoryTriggerExit", OnItemFactoryTriggerExit);
		}

		void OnItemFactoryTriggerEnter(string str)
		{
			messageBox.text = str;
			messageBox.enabled = true;
		}

		void OnItemFactoryTriggerExit()
		{
			messageBox.enabled = false;
			messageBox.text = string.Empty;
		}

		void Initialize()
		{
			messageBox.enabled = false;
			messageBox.text = string.Empty;
		}
	}
}