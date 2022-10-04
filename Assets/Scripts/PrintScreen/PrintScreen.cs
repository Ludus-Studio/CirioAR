using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ludus.PrintScreenCapture
{
    public class PrintScreen : MonoBehaviour
    {
        public GameObject myUI;
        
        [Space]
        public Animator myPhotoAnimator;
        public string shotAnimationTrigger = "shot";
        public AudioSource printSound;

        [Space]
        public UnityEvent BeforeTakePrint;
        public UnityEvent<Texture2D> OnPrintTaken;

        private Texture2D currentTexTaken = null;
        private ToggleAllCanvasVisualization canvasVisualization;
        public Texture2D CurrentTexTaken => currentTexTaken;

        private void Start()
        {
            canvasVisualization = GetComponent<ToggleAllCanvasVisualization>();
        }

        public void Reset()
        {
            currentTexTaken = null;
        }

        public void TakePrint()
        {
            StartCoroutine(TakePrintDelay());
        }
        IEnumerator TakePrintDelay()
        {
            // Desliga toda a UI na hora de tirar o print
            myUI.SetActive(false);
            if(canvasVisualization) canvasVisualization.ToggleCanvasVisualization(false);
            
            // Evento antes de tirar foto
            BeforeTakePrint.Invoke();

            yield return new WaitForEndOfFrame();

            //captura a tela
            currentTexTaken = ScreenCapture.CaptureScreenshotAsTexture(2);
            Debug.Log("PrintScreen tirado", this);

            yield return new WaitForEndOfFrame();
            
            // Animação de foto
            if (myPhotoAnimator) myPhotoAnimator.SetTrigger(shotAnimationTrigger);
            if(printSound) printSound.Play();
            yield return new WaitForSeconds(.5f); // espera o tempo da animação de foto

            // Ativa novamente a interface
            myUI.SetActive(true);
            if (canvasVisualization) canvasVisualization.ToggleCanvasVisualization(true);
            
            // Evento depois de tirar foto
            OnPrintTaken.Invoke(currentTexTaken);
        }
    }
}

