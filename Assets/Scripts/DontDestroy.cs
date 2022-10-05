using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    public GameObject[] prefabs;
    public string[] texts;
    public TextMeshProUGUI textMesh;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void ChangePrefab(int prefabID)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabs[i].SetActive(false);

        }

        if(prefabID < texts.Length)textMesh.text = texts[prefabID];
        prefabs[prefabID].SetActive(true);
    }
}
