using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private GameObject _blackBGObject;
    private Image _blackBGImage;
    //private bool _isClicked;
    
    private Coroutine _fadeOutCoroutine;
    private Coroutine _fadeInAndLoadSceneCoroutine;
    
    private void Start()
    {
        _blackBGImage = _blackBGObject.GetComponent<Image>();
        _blackBGObject.SetActive(true);
        //_isClicked = false;
        SetBGAlpha(1);
        CallFadeOut();
    }
    
    private void ExitGame()
    {
        Application.Quit();
    } 
    public void CallExitGame(){ExitGame();}
    
    private void SetBGAlpha(float value)
    {
        Color tmpColor = _blackBGImage.color;
        tmpColor.a = value;
        _blackBGImage.color = tmpColor;
    }
    
    private IEnumerator FadeOut()
    {
        if (_blackBGObject != null && _blackBGImage != null && _blackBGObject.activeSelf)
        {
            Debug.Log("Fading Out...");
            yield return new WaitForSeconds(0.5f);
            _blackBGImage.CrossFadeAlpha(0, _fadeDuration, false);
            yield return new WaitForSeconds(_fadeDuration + 0.1f);
            
            _blackBGObject.SetActive(false);
        }
    }
    public void CallFadeOut()
    {
        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
            _fadeOutCoroutine = null;
        }
        _fadeOutCoroutine = StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeInAndLoadScene(string sceneName)
    {
        if (_blackBGObject != null && _blackBGImage != null && !_blackBGObject.activeSelf)
        {
            Debug.Log("Fading In...");
            _blackBGObject.SetActive(true);
        
            _blackBGImage.CrossFadeAlpha(1, _fadeDuration, false);
            yield return new WaitForSeconds(_fadeDuration + 0.5f);

            SceneManager.LoadScene(sceneName);
        }
    }
    public void CallFadeInAndLoadScene(string sceneName)
    {
        if (_fadeInAndLoadSceneCoroutine != null)
        {
            //StopCoroutine(_fadeInAndLoadSceneCoroutine);
            //_fadeInAndLoadSceneCoroutine = null;
            Debug.Log("Already clicked!");
            return;
        }
        _fadeInAndLoadSceneCoroutine = StartCoroutine(FadeInAndLoadScene(sceneName));
    }
}
