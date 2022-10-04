using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ludus.PrintScreenCapture
{
    [RequireComponent(typeof(PrintScreen))]
    public class SaveScreen : MonoBehaviour
    {
        [SerializeField, Tooltip("salvar a foto assim que o print é tirado")]
        private bool autoSave = true;
        [Space, SerializeField]
        private string screenShotName = "Image";
        [SerializeField]
        private string extension = ".jpg";
        [SerializeField]
        private string album = "Screenshots";

        [Space]
        public UnityEvent<string> OnScreenSaved;

        private string currentSavePath = null;

        private PrintScreen myPrintScreen;
       
        public bool AutoSave => autoSave;
        public string CurrentSavePath => currentSavePath;
        public void Reset()
        {
            if(myPrintScreen) myPrintScreen.Reset();
            currentSavePath = null;
        }
        private void OnEnable()
        {
            myPrintScreen = GetComponent<PrintScreen>();
            if (autoSave) myPrintScreen.OnPrintTaken.AddListener(SavePrintScreenTex);
        }

        public void SavePrintScreenTex(Texture2D printTex)
        {
            currentSavePath = GetAndroidExternalStoragePath();
            // Salva a textura como um arquivo da extensão configurada
            File.WriteAllBytes(currentSavePath, printTex.EncodeToJPG());
            Debug.Log($"Captura de tela salva no caminho ->  {currentSavePath}", this);

            OnScreenSaved.Invoke(currentSavePath);
        }

        public void SavePrintScreen()
        {
            Texture2D myTex = myPrintScreen.CurrentTexTaken;
            if (myTex != null)
                SavePrintScreenTex(myTex);
        }

        // pega o caminho pra salvar a foto na galeria do Android
        public string GetAndroidExternalStoragePath()
        {
            if (Application.platform != RuntimePlatform.Android)
                return Application.persistentDataPath;

            var jc = new AndroidJavaClass("android.os.Environment");
            var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory",
                jc.GetStatic<string>("DIRECTORY_DCIM"))
                .Call<string>("getAbsolutePath");
            return path + $"/{album}/{screenShotName}_{System.DateTime.Now:ddMMyyyyHHmmss}{extension}";
        }

        private void OnDisable()
        {
            if (autoSave) myPrintScreen.OnPrintTaken.RemoveListener(SavePrintScreenTex);
        }
    }
}