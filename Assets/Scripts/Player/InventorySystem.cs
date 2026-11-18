
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public enum ItemType
    {
        HospitalKey,
        BasementKey,
        OfficeKey,
        Battery,
        MedicalKit
    }

    [Header("Inventar")]
    public int maxSlots = 5;

    private List<ItemType> items = new List<ItemType>();

    public int ItemCount => items.Count;

    public bool AddItem(ItemType item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventar ist voll!");
            return false;
        }

        items.Add(item);

        Debug.Log("Gegenstand eingesammelt: " + item);
        return true;
    }

    public bool HasItem(ItemType item)
    {
        return items.Contains(item);
    }

    public bool RemoveItem(ItemType item)
    {
        return items.Remove(item);
    }

    public List<ItemType> GetItems()
    {
        return new List<ItemType>(items);
    }

    public void ClearInventory()
    {
        items.Clear();
    }
}
