using LJS.Inventories;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LJS.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _amountText;
        
        public RectTransform RectTrm => transform as RectTransform;
        public InventoryItem item;
        
        public void UpdateSlot(InventoryItem newItem)
        {
            item = newItem;
            _image.color = Color.white;
            if (item != null)
            {
                _image.sprite = item.data.icon;

                if (item.stackSize > 1)
                    _amountText.text = item.stackSize.ToString();
                else
                    _amountText.text = string.Empty;
            }
        }

        public void CleanUpSlot()
        {
            item = null;
            _image.sprite = null;
            _image.color = Color.clear;
            _amountText.text = string.Empty;
        }
    }
}
