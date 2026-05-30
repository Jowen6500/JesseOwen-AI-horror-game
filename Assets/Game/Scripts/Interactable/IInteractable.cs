using UnityEngine;

//interface to tag object that will be interactable
public interface IInteractable
{
    public string Name { get; }//creating its properties
    public void Interact();//creating its method
}
