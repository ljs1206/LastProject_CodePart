using UnityEngine;

namespace LJS.Items
{
    public interface IPickable
    {
        public void PickUp(Collider2D picker);
    }
}
