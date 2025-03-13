using LJS.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LJS.UI
{
    public class DescriptionUI : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemTitleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        public void SetItemData(ItemDataSO itemData)
        {
            if (itemData == null)
            {
                _itemImage.sprite = null;
                _itemImage.color = Color.clear;
                _descriptionText.text = string.Empty;
                _itemTitleText.text = string.Empty;
            }
            else
            {
                _itemImage.sprite = itemData.icon;
                _itemImage.color = Color.white;
                _descriptionText.text = itemData.GetDescription();
                _itemTitleText.text = itemData.itemName;
            }
        }
    }
}
