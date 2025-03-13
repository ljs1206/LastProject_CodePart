using System;
using System.Collections.Generic;
using System.Linq;
using LJS.Entities;
using LJS.EventSystem;
using LJS.Inventories;
using LJS.Items;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerInventoryData : InventoryData, IAfterInitable
    {
        [SerializeField] private GameEventChannelSO _inventoryEventChannel;
        
        public void AfterInit()
        {
            _inventoryEventChannel.AddListener<RequestInventoryData>(HandleRequestInventoryData);
        }

        private void OnDestroy()
        {
            _inventoryEventChannel.RemoveListener<RequestInventoryData>(HandleRequestInventoryData);
        }

        private void HandleRequestInventoryData(RequestInventoryData evt)
        {
            UpdateInventoryUI();
        }

        public override void AddItem(ItemDataSO itemData, int count = 1)
        {
            IEnumerable<InventoryItem> items = GetItems(itemData);
            //지정한 아이템에 해당하는 아이템을 모두 가져온다.

            InventoryItem canAddItem = items.FirstOrDefault(item => item.IsFullStack == false);

            if (canAddItem == null)
            {
                CreateNewInventoryItem(itemData, count);
            }
            else
            {
                int remain = canAddItem.AddStack(count);
                if(remain > 0)
                    CreateNewInventoryItem(itemData, remain);
                //만약 한번에 드랍되는 양이 스택을 초과할만큼 드랍되는걸로도 기획되면 while루프를 돌려야한다.
            }

            UpdateInventoryUI();
        }

        private void CreateNewInventoryItem(ItemDataSO itemData, int count)
        {
            InventoryItem invenItem = new InventoryItem(itemData, count);
            inventory.Add(invenItem);
        }
        
        private void UpdateInventoryUI()
        {
            InventoryDataList evt = InventoryEvents.InventoryDataList;
            evt.items = inventory;
            _inventoryEventChannel.RaiseEvent(evt);
        }

        public override void RemoveItem(ItemDataSO itemData, int count)
        {
            //여기에 CanRemove호출 파트가 들어간다.
            
            GetItem(itemData).RemoveStack(count);
            UpdateInventoryUI();
        }

        public override bool CanAddItem(ItemDataSO itemData)
        {
            return true;
        }

        public override bool CanRemoveItem(ItemDataSO itemData, int count)
        {
            return true;
        }


    }
}
