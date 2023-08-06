using System.Collections.Generic;

public static class InventoryManager
{
    private static Dictionary<string, int> inventory = new Dictionary<string, int>();

    public static void AddItem(string itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += quantity;
        }
        else
        {
            inventory.Add(itemName, quantity);
        }
    }

    public static void RemoveItem(string itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] -= quantity;
            if (inventory[itemName] <= 0)
            {
                inventory.Remove(itemName);
            }
        }
    }

    public static int GetItemCount(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            return inventory[itemName];
        }
        return 0;
    }

    public static Dictionary<string, int> GetInventory()
    {
        return inventory;
    }
}

