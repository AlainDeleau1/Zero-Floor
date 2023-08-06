using UnityEngine;
using System.Collections.Generic;

public class InventorySaveLoad : MonoBehaviour
{
    public void SaveInventory()
    {
        Dictionary<string, int> inventory = InventoryManager.GetInventory();
        foreach (var item in inventory)
        {
            PlayerPrefs.SetInt(item.Key, item.Value);
        }
    }

    public void LoadInventory()
    {
        Dictionary<string, int> inventory = InventoryManager.GetInventory();
        foreach (var item in inventory)
        {
            int quantity = PlayerPrefs.GetInt(item.Key, 0);
            InventoryManager.AddItem(item.Key, quantity);
        }
    }
}

