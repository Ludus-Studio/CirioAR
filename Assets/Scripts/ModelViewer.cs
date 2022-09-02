using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelViewer : MonoBehaviour
{
    [Header("Buttons")]
    public Button leftButton;
    public Button rightButton;

    int modelIndex;

    [Header("Starts in 0, exclusive")]
    public int maxModels;

    [Header("Game Object holding all models")]
    public GameObject models;
    GameObject lastActiveModel;
    [Space]
    public TMP_Text infoText;
    [TextArea(4, 7)]
    [Header("Info Texts, in models order from hierarchy")]
    public string[] descriptionTexts;

    private void Start()
    {
        models.transform.GetChild(modelIndex).gameObject.SetActive(true);
        lastActiveModel = models.transform.GetChild(modelIndex).gameObject;
    }

    private void Update()
    {
        UpdateButtons();

        infoText.text = "" + descriptionTexts[modelIndex];
    }

    void UpdateButtons()
    {
        if (modelIndex - 1 == -1)
        {
            leftButton.interactable = false;
        }
        else leftButton.interactable = true;

        if (modelIndex == maxModels)
        {
            rightButton.interactable = false;
        }
        else rightButton.interactable = true;
    }

    public void LeftButton()
    {
        modelIndex--;
        lastActiveModel.SetActive(false);
        models.transform.GetChild(modelIndex).gameObject.SetActive(true);
        lastActiveModel = models.transform.GetChild(modelIndex).gameObject;
    }

    public void RightButton()
    {
        modelIndex++;
        lastActiveModel.SetActive(false);
        models.transform.GetChild(modelIndex).gameObject.SetActive(true);
        lastActiveModel = models.transform.GetChild(modelIndex).gameObject;
    }
}