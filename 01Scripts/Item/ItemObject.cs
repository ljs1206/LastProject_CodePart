using System;
using LJS.Entities;
using LJS.Inventories;
using UnityEngine;

namespace LJS.Items
{
    public class ItemObject : MonoBehaviour, IPickable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ItemDataSO _itemData;
        
        private Rigidbody2D _rbCompo;

        private void OnValidate()
        {
            if (_itemData == null) return;
            if (_spriteRenderer == null) return;
            
            _spriteRenderer.sprite = _itemData.icon;
            gameObject.name = $"Item_[{_itemData.itemName}]";
        }

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }

        public void SetUpItem(ItemDataSO itemData, Vector2 velocity)
        {
            _itemData = itemData;
            _rbCompo.linearVelocity = velocity;
            _spriteRenderer.sprite = itemData.icon;
        }


        public void PickUp(Collider2D picker)
        {
            //이부분을 사실 이벤트로 처리하는게 더 맞는데

            if (picker.TryGetComponent(out Entity entity))
            {
                InventoryData invenData = entity.GetCompo<InventoryData>(true);
                invenData.AddItem(_itemData);
                Destroy(gameObject);
            }
        }
    }
}
