using System.Collections.Generic;
using System.Linq;
using LJS.Entities;
using LJS.Items;
using UnityEngine;

namespace LJS.Inventories
{
    public abstract class InventoryData : MonoBehaviour, IEntityComponent
    {
        public List<InventoryItem> inventory;

        protected Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            inventory = new List<InventoryItem>();
        }
        
        //하나만 가져올 때
        public virtual InventoryItem GetItem(ItemDataSO itemData)
            => inventory.FirstOrDefault(inventoryItem => inventoryItem.data == itemData);
        
        public virtual IEnumerable<InventoryItem> GetItems(ItemDataSO itemData)
            => inventory.Where(inventoryItem => inventoryItem.data == itemData);

        public abstract void AddItem(ItemDataSO itemData, int count = 1);
        public abstract void RemoveItem(ItemDataSO itemData, int count);
        public abstract bool CanAddItem(ItemDataSO itemData);
        public abstract bool CanRemoveItem(ItemDataSO itemData, int count);

        [ContextMenu("Print inventory")]
        private void PrintInventory()
        {
            foreach (InventoryItem invenItem in inventory)
            {
                Debug.Log($"{invenItem.data.itemName} : {invenItem.stackSize}");
            }
        }
    }
}
