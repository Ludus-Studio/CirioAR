using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ludus.PrintScreenCapture
{
    public class ShowPrint : MonoBehaviour
    {
        public SaveScreen myPrintScreenSave;
        
        [Space]
        public GameObject fotoImageGO;

        private Image myFotoImage;
        private void Start()
        {
            myFotoImage = fotoImageGO.GetComponent<Image>();
            fotoImageGO.SetActive(false);
        }

        public void EnterVisualization(Texture2D imgTex)
        {
            myFotoImage.material.mainTexture = imgTex;
            fotoImageGO.SetActive(true);
        }

        public void ExitVisualization()
        {
            fotoImageGO.SetActive(false);
            myFotoImage.material.mainTexture = null;
            myPrintScreenSave.Reset();
        }
    }
}