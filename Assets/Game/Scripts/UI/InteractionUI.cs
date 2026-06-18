using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactionInfo;
    [SerializeField] private TMP_Text _objectName;

    private void SetVisible(bool value)
    {
        _interactionInfo.SetActive(value);
    }
    public void CallSetVisible(bool value){_interactionInfo.SetActive(value);}

    private void SetObjectName(string txt)
    {
        _objectName.text = txt;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_interactionInfo.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
    }
    public void CallSetObjectName(string txt){SetObjectName(txt);}
}
