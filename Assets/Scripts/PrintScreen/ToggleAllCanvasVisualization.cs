using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludus.PrintScreenCapture
{
    [RequireComponent(typeof(Canvas))]
    public class ToggleAllCanvasVisualization : MonoBehaviour
    {
        [SerializeField]
        private List<Canvas> canvasExceptionList = new List<Canvas>();


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

        // Desativa ou ativa todos os canvas da cena menos o próprio canvas e os que estão na lista de exceção
        public void ToggleCanvasVisualization(bool val)
        {
            canvasList = FindObjectsOfType<Canvas>(true);
            foreach (Canvas canvas in canvasList)
            {
                if (!canvas.Equals(myCanvas) && !canvasExceptionList.Contains(canvas))
                {
                    canvas.gameObject.SetActive(val);
                }
            }
            currentVisualizationValue = val;
        }
    }
}
