using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SFStudio.OpenWorld.UI
{
	public class ItemElement : MonoBehaviour
	{
		[SerializeField] TextMeshProUGUI text = default;
		[SerializeField] Button button = default;
		[SerializeField] Image image = default;

		Item item;
		
		public void Initialize(Item item)
		{
			this.item = item;
			
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(OnClick);
			
			Repaint();
		}

		void Repaint()
		{
			text.text = $"{item.itemDefinition.name} x{item.amount:N0}";
			image.sprite = item.itemDefinition.thumbnail;
		}

		void OnClick()
		{
			item.owner.DropItem(item, item.amount);
			item = null;
		}
	}
}