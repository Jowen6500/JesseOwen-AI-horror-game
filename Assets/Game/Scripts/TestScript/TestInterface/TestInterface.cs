using UnityEngine;

public class TestInterface : MonoBehaviour
{
    //interface example
    //Only declare function, no block of code, no var also.
    //in interface, function is abstract and public
    //interface will be used only by other classes, using its abstract function and properties
    public interface ITestInteractable//creating interface Interactable
    {
        public string Name {get;}//creating its properties
        public void Interact();//creating its function/method
    }

    public class Door : ITestInteractable
    {
        [SerializeField] protected string _name;//var for property's name
        public string Name => _name;//this means get "_name" value and assign it into the "Name" property inside the IInteractable

        public void Interact()
        {
            //type block of codes here
        }
    }
    //in inheritance, child class can only have one parent. interfaces enables child class to have more that one parents.
}