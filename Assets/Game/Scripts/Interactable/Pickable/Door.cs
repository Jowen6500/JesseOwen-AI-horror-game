using UnityEngine;

//door script
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    public string Name => _name;//get "_name" value and assign it into the "Name" property inside the IInteractable
    
    public void Interact()//abstract function interact
    {
        throw new System.NotImplementedException();
    }
}
