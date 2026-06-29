using UnityEngine;
using UnityEngine.Events;

//item script
public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ItemData _itemData;//var to determine item data
    public string Name => _itemData.Name;//get "_itemData.Name" value and assign it into the "Name" property inside the IInteractable
    public UnityEvent OnItemPicked;//unity event var, used to tell other module/class that there's an item being picked up

    public virtual void Pickup(PlayerCharacter character)//Pickup Method virtual so child can override it
    {
        ItemData newData = new ItemData(_itemData.ID, _itemData.Name);//create a copy of "_itemData"
        // Menambahkan salinan data ke list di inventory
        // menggunakan reference PlayerCharacter
        character.Inventory.AddItems(newData);//call "AddItems" method from "InventoryManager" through "PlayerCharacter"
        OnItemPicked?.Invoke();//invoke "OnItemPicked" to call pickup item event
        Destroy(gameObject);//destroy its game object(after the code above)
    }
    
    //created context menu so the interact function can be called through the door component inside the inspector
    [ContextMenu("Interact Item")]
    public void Interact(PlayerCharacter character)//Interact method
    {
        Pickup(character);//call pickup method
    }
}
