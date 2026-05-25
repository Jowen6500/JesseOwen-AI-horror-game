using System;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    //MonoBehaviour class primarily provides access to Unity Messages (often called "lifecycle methods")
    //      and several Public Methods and Properties used to manage components and game objects.
    //Methods and lifecycle hooks provided by MonoBehavior in Unity 6
    
    //Initialization Lifecycle (Magic Methods)
    private void Awake(){}//Called when the script instance is loaded (runs before Start and before any GameObject is active)
    private void Start()//Called on the frame the script is enabled, just before any of the update methods are called for the first time
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i);
        }
    }
    private void Reset(){}//Called when the script is attached to an object for the first time or when the Reset command is used in the Inspector, setting default values
    
    //Frame-based Updates
    private void Update(){}//Called every frame; the standard method for most game logic, movement, and input collection
    private void FixedUpdate(){}//Called at a fixed frame rate (independent of the rendering frame rate). It is ideal for Physics and Rigidbody calculations
    private void LateUpdate(){}//Called after all Update methods have finished. It is typically used for third-person cameras or procedural animation to ensure calculations are done after movement
    
    //State & Enabling Hooks
    private void OnEnable(){}//Called when the object becomes enabled and active (happens right before Start if the object is initially active)
    private void OnDisable(){}//Called when the behavior becomes disabled or inactive. Ideal for unsubscribing from events
    private void OnDestroy(){}//Called when the object/script is being destroyed (runs at the very end of the frame or when scene loading ends)

    /*Naming Identifier for different access modifier
    private int _myInt;
    public int MyInt;
    protected int _myInt;
    private/public/protected void Method(){}//same for everything on methods
    */

    //
    static void Main(string[] args)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(i);
        }
    }
}
