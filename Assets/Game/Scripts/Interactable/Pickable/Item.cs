using UnityEngine;

//item script
public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ItemData _itemData;//var to determine item data
    public string Name => _itemData.Name;//get "_itemData.Name" value and assign it into the "Name" property inside the IInteractable
    
    public void Interact()//Interact method
    {
        Pickup();
    }

    public void Pickup()//Pickup Method
    {
        throw new System.NotImplementedException();
    }
}
