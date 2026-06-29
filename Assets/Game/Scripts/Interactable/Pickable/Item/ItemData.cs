using UnityEngine;

//this class is created to store item data
[System.Serializable] public class ItemData//serialized class, enables us to tweak its var value through inspector
{
    public string ID;//var for id
    public string Name;//var for name

    public ItemData(string id, string name)//constructor fot ItemData to assign its var value
    {
        ID = id;
        Name = name;
    }
}
