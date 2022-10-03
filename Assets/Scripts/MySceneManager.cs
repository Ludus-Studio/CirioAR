using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public string selfieScene;
    public string cameraScene;

    [NaughtyAttributes.ReadOnly]
    public string currentSceneName;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void ChangeScene(string sceneName)
    {
        currentSceneName = sceneName;
        SceneManager.LoadScene(sceneName);
    }
    public void ToggleCameraScene()
    {
        if(currentSceneName.Equals(cameraScene))
            ChangeScene(selfieScene);
        else
            ChangeScene(cameraScene);
    }
}
