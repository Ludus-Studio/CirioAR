using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using NaughtyAttributes;

[RequireComponent(typeof(ARPlaneManager))]
public class TogglePlaneVisualizer : MonoBehaviour
{
    [SerializeField]
    private bool defaultValue;

    private ARPlaneManager planeManager;
    private bool visualizerEnabled;

    private void OnEnable()
    {
        planeManager = GetComponent<ARPlaneManager>();// pega componente ARRPlaneManager
        SetPlaneVisualizer(defaultValue); // ajusta visualização ao iniciar
        planeManager.planesChanged += AdjustPlanes;

    }

    private void ToggleVisualizer()
    {
        // inverte a visualização dos planos
        SetPlaneVisualizer(!visualizerEnabled);
    }

    public void SetPlaneVisualizer(bool val)
    {
        visualizerEnabled = val;
        // busca todos os planos e desliga ou liga a visualização
        foreach(var plane in planeManager.trackables)
            plane.gameObject.SetActive(val);
    }
    // verifica se há novos planos e desliga eles
    private void AdjustPlanes(ARPlanesChangedEventArgs args)
    {
        if (visualizerEnabled) return;
        foreach(var newPlanes in args.added)
            newPlanes.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= AdjustPlanes;
    }
}
