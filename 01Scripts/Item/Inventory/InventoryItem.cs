using LJS.Items;

namespace LJS.Inventories
{
    public class InventoryItem
    {
        public ItemDataSO data;
        public int stackSize;

        public bool IsFullStack => stackSize >= data.maxStack;

        public InventoryItem(ItemDataSO newItemData, int count = 1)
        {
            data = newItemData;
            stackSize = count;
        }

        public int AddStack(int count)
        {
            int remainCount = 0;
            stackSize += count;

            if (stackSize > data.maxStack)
            {
                remainCount = stackSize - data.maxStack;
                stackSize = data.maxStack;
            }

            return remainCount;
        }

        public void RemoveStack(int count = 1)
        {
            stackSize -= count; //뺄 수 있는지 여부는 바깥쪽에서 판단해서 수행한다.
        }
    }
}
