
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventorySystem.ItemType itemType;

    private bool collected;

    public void Collect(InventorySystem inventory)
    {
        if (collected || inventory == null)
            return;

        if (inventory.AddItem(itemType))
        {
            collected = true;
            gameObject.SetActive(false);
        }
    }
}
