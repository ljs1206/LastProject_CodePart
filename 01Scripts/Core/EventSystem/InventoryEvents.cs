using System.Collections.Generic;
using LJS.Inventories;
using UnityEngine;

namespace LJS.EventSystem
{
    public static class InventoryEvents
    {
        public static readonly InventoryDataList InventoryDataList = new InventoryDataList();
        public static readonly RequestInventoryData RequestInventoryData = new RequestInventoryData();
    }


    public class InventoryDataList : GameEvent
    {
        public List<InventoryItem> items;
    }

    public class RequestInventoryData : GameEvent
    { }
    
}
