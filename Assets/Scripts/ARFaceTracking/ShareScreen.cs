using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class ShareScreen : MonoBehaviour
{
    public GameObject ui;
    public string shotAnimationTrigger = "shot";

    [Space, SerializeField]
    private string ScreenShotName = "ARCirio";
    [SerializeField]
    private string albumName = "CirioAR";
    [SerializeField, NaughtyAttributes.ResizableTextArea]
    private string ScreenShotText = "#ARCírio";
    private Animator uiAnimator;

    [Space]
    public UnityEvent OnDeactiveCallback;
    public UnityEvent OnActiveCallback;

    private void Awake()
    {
        uiAnimator = ui.GetComponent<Animator>();
        NativeToolkit.OnScreenshotSaved += GetSavedData;
    }

    public void PrintAndSave()
    {
        Debug.Log("Clicou no Botão");
        StartCoroutine(PrintDelay());
        
    }

    IEnumerator PrintDelay()
    {
        OnDeactiveCallback.Invoke();
        ui.SetActive(false);
        yield return new WaitForEndOfFrame();
        NativeToolkit.SaveScreenshot(ScreenShotName + "_" + Random.Range(0, 10000).ToString("00000"), albumName);
        yield return new WaitForEndOfFrame();
        OnActiveCallback.Invoke();
        ui.SetActive(true);
        uiAnimator.SetTrigger(shotAnimationTrigger);
    }

    public void GetSavedData(string data)
    {
        Debug.Log("||||||||||||||||||||DADOS SALVOS ||||||||||||||||||");
        Debug.Log(data);
        Debug.Log("||||||||||||||||||||||||||||||||||||||");
    }

    private void OnDestroy()
    {
        NativeToolkit.OnScreenshotSaved -= GetSavedData;
    }
}
