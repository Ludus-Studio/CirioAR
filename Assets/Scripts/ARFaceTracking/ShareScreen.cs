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
    private string ScreenShotName = "Image.png";
    [SerializeField]
    private string ScreenShotText = "#ARCírio";
    private Animator uiAnimator;

    [Space]
    public UnityEvent OnDeactiveCallback;
    public UnityEvent OnActiveCallback;

    private void Awake()
    {
        uiAnimator = ui.GetComponent<Animator>();
    }

    public void PrintAndShare()
    {
        Debug.Log("Clicou no Botão");
        string path = Print();
        Share(path);
    }

    private string Print()
    {
        string screenshotPath = Application.persistentDataPath + "/" + ScreenShotName;

        if(File.Exists(screenshotPath))
            File.Delete(screenshotPath);

        StartCoroutine(PrintDelay());
        Debug.Log("Print path = " + screenshotPath);
        return screenshotPath;
    }
    IEnumerator PrintDelay()
    {
        OnDeactiveCallback.Invoke();
        ui.SetActive(false);
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot(ScreenShotName);
        yield return new WaitForEndOfFrame();
        OnActiveCallback.Invoke();
        ui.SetActive(true);
        uiAnimator.SetTrigger(shotAnimationTrigger);
    }

    private void Share(string screenShotPath)
    {
        StartCoroutine(ShareDelay(screenShotPath));
    }

    IEnumerator ShareDelay(string screenShotPath)
    {
        Debug.Log("entrou no share");
        while (!File.Exists(screenShotPath))
            yield return new WaitForEndOfFrame();
        Debug.Log("Verificou arquivo");
        yield return new WaitForSeconds(.5f); // espera o tempo da animação de foto

        new NativeShare().AddFile(screenShotPath)
        .SetText(ScreenShotText)
        .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
        .Share();
        Debug.Log("Compartilhou");
    }
}
