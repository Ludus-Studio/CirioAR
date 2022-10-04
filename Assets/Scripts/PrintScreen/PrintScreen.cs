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
        public bool autoEnableUIAfterPhoto = false;
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
            InterfaceVisualization(false);
            
            // Evento antes de tirar foto
            BeforeTakePrint.Invoke();

            yield return new WaitForEndOfFrame();

            //captura a tela
            currentTexTaken = ScreenCapture.CaptureScreenshotAsTexture(2);
            Debug.Log("PrintScreen tirado", this);

            yield return new WaitForEndOfFrame();
            
            // Animação de foto
            if(printSound) printSound.Play();
            if (myPhotoAnimator) myPhotoAnimator.SetTrigger(shotAnimationTrigger);
            yield return new WaitForSeconds(.5f); // espera o tempo da animação de foto

            // Ativa novamente a interface de configurada para automático
            if(autoEnableUIAfterPhoto) InterfaceVisualization(true);

            // Evento depois de tirar foto
            OnPrintTaken.Invoke(currentTexTaken);
        }

        // Desliga ou liga toda a UI
        public void InterfaceVisualization(bool val)
        {
            myUI.SetActive(val);
            if (canvasVisualization) canvasVisualization.ToggleCanvasVisualization(val);
        }
    }
}

