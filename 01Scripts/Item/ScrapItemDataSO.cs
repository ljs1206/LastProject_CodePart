using UnityEngine;

namespace LJS.Items
{
    [CreateAssetMenu(fileName = "ScrapItemDataSO", menuName = "SO/Items/ScrapItem")]
    public class ScrapItemDataSO : ItemDataSO
    {
        [TextArea] public string description;

        public override string GetDescription()
        {
            return description;
        }
    }
}
