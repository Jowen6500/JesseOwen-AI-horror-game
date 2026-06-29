using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private List<ItemData> _item = new List<ItemData>();//List var with string as its data type to store picked item's data
    public List<ItemData> Items => _item;//property to get "_item"(because its private) list value
 
    public void AddItems(ItemData item)//method to add items into "_item" list
    {
        Items.Add(item);//code to add new data to the list
    }
 
    public bool CheckItem(string id)//method to check items inside "_item" list
    {
        // Mencari apakah ada itemdata di dalam list 
        // yang id nya sama dengan id yang ditentukan di parameter.
        // Jika ketemu akan bernilai true, jika tidak akan bernilai false.
        bool isExsists = Items.Exists(itemData => string.Equals(itemData.ID, id));
        return isExsists;//return the "isExists" data
    }
 
    public void RemoveItem(ItemData item)//method to delete/remove items from "_item" list
    {
        Items.Remove(item);
    }
}
