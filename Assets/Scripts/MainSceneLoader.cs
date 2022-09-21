using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneLoader : MonoBehaviour
{
    [SerializeField]
    private MySceneManager m_SceneManager;
    
    
    [SerializeField]
    private string m_SceneName = "FaceTraking";

    void Start()
    {
        StartCoroutine(WaitToChangeScene());
    }

    IEnumerator WaitToChangeScene()
    {
        yield return new WaitUntil(()=> m_SceneManager);
        yield return new WaitForEndOfFrame();
        m_SceneManager.ChangeScene(m_SceneName);
    }
}
