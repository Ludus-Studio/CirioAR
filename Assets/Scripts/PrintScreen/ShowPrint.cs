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
        public GameObject fotoSceneGO;

        [SerializeField]
        private Image myFotoImage;
        private void Start()
        {
            fotoSceneGO.SetActive(false);
        }

        public void EnterVisualization(Texture2D imgTex)
        {
            myFotoImage.material.mainTexture = imgTex;
            fotoSceneGO.SetActive(true);
        }

        public void ExitVisualization()
        {
            fotoSceneGO.SetActive(false);
            myFotoImage.material.mainTexture = null;
            myPrintScreenSave.PrintScreen.InterfaceVisualization(true);
            myPrintScreenSave.Reset();
        }
    }
}