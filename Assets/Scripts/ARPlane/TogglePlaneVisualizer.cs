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
    private int trackablesCount = 0;
    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();// pega componente ARRPlaneManager
        button.onClick.AddListener(ToggleVisualizer);// diciona listener no botão
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();// pega texto para modificar
        SetPlaneVisualizer(defaultValue); // ajusta visualização ao iniciar
    }

    private void ToggleVisualizer()
    {
        // inverte a visualização dos planos
        SetPlaneVisualizer(!visualizerEnabled);
    }

    private void SetPlaneVisualizer(bool val)
    {
        // busca todos os planos e desliga a visualização
        foreach(var plane in planeManager.trackables)
            plane.gameObject.SetActive(val);

        // ajusta o contador de planos para saber se novos foram adicionados
        if(val)
            trackablesCount = planeManager.trackables.count;
        else
            trackablesCount = 0;

        // ajusta o botão
        ChangeButtonText(val);
        visualizerEnabled = val;
    }

    private void Update()
    {
        // verifica se algum plano foi adicionado e remove a visualização do novo plano
        if (visualizerEnabled == false && trackablesCount < planeManager.trackables.count)
            SetPlaneVisualizer(false);
    }

    // muda o texto do botão
    private void ChangeButtonText(bool val)
    {
        if (val)
            buttonText.text = buttonOnText;
        else
            buttonText.text = buttonOffText;
    }

    // remove os listeners do botão ao destruir o objeto
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
