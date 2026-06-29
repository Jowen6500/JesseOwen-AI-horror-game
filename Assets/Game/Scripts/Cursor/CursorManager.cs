using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager _instance;
    
    public static CursorManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    
    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    } 
    public void CallShowCursor(){ShowCursor();}

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CallHideCursor(){HideCursor();}
}
