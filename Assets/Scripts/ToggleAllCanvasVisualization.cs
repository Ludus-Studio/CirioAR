using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ToggleAllCanvasVisualization : MonoBehaviour
{
    private Canvas myCanvas;
    private bool currentVisualizationValue = true;
    private Canvas[] canvasList;
    private void Awake()
    {
        myCanvas = GetComponent<Canvas>();
    }

    public void ToggleCanvasVisualization()
    {
        ToggleCanvasVisualization(!currentVisualizationValue);
    }

    public void ToggleCanvasVisualization(bool val)
    {
        canvasList = FindObjectsOfType<Canvas>(true);
        foreach (Canvas canvas in canvasList)
        {
            if (!canvas.Equals(myCanvas))
            {
                canvas.gameObject.SetActive(val);
            }
        }
        currentVisualizationValue = val;
    }
}
