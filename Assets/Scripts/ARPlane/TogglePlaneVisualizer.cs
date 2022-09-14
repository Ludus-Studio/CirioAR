using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;
using NaughtyAttributes;

public class TogglePlaneVisualizer : MonoBehaviour
{
    [SerializeField]
    private bool defaultValue;
    [SerializeField]
    private Button button;
    [SerializeField]
    private string buttonOnText = "Desligar Plano";
    [SerializeField]
    private string buttonOffText = "Visualizar Plano";

    private TextMeshProUGUI buttonText;

    private ARPlaneManager planeManager;
    private bool visualizerEnabled;
    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        button.onClick.AddListener(ToggleVisualizer);
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        SetPlaneVisualizer(defaultValue);
    }

    private void ToggleVisualizer()
    {
        SetPlaneVisualizer(!visualizerEnabled);
    }

    private void SetPlaneVisualizer(bool val)
    {
        foreach(var plane in planeManager.trackables)
            plane.gameObject.SetActive(val);
        
        ChangeButtonText(val);
        visualizerEnabled = val;
    }

    private void ChangeButtonText(bool val)
    {
        if (val)
            buttonText.text = buttonOnText;
        else
            buttonText.text = buttonOffText;
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
