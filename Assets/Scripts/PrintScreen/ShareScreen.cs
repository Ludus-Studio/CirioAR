using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

namespace Ludus.PrintScreenCapture
{
    [RequireComponent(typeof(SaveScreen))]
    [RequireComponent(typeof(PrintScreen))]
    public class ShareScreen : MonoBehaviour
    {
        // ESSA CLASSE NÃO FUNCIONA SEM O PLUGIN (NativeShare)
        //
        //https://assetstore.unity.com/packages/tools/integration/native-share-for-android-ios-112731
        //
        ////////////////////////////////////////////////////////////////

        [SerializeField, Tooltip("Compartilhar a foto assim que o print é tirado")]
        private bool autoShare = true;

        [Space, SerializeField]
        private string ScreenShotText = "Minha frase de compartilhamento aqui.";

        [Space]
        public UnityEvent OnSharedCallback;


        private SaveScreen mySaveScreen;
        private PrintScreen myPrintScreen;
        private void OnEnable()
        {
            myPrintScreen = GetComponent<PrintScreen>();
            mySaveScreen = GetComponent<SaveScreen>();


            // verifica o auto save e ajusta os listeners
            if (!autoShare) return;
            mySaveScreen.OnScreenSaved.AddListener(SharePath);
            // caso o autoSave não estiver ligado e o autoShare sim, ativa o callback de uma função que salva a textura antes de compartilhar
            if (!mySaveScreen.AutoSave) myPrintScreen.OnPrintTaken.AddListener(ShareTex);
        }

        // compartilha a foto que já foi salva no SaveScreen ou salva e compartilha
        public void Share()
        {
            string myPath = mySaveScreen.CurrentSavePath;
            // se a foto ja foi salva compartilha o caminho salvo
            if (myPath != null)
                SharePath(myPath);
            else // caso não, salva a foto e compartilha
            {
                mySaveScreen.SavePrintScreen();
                if (!autoShare)// se o autoShare estiver ligado a função SharePath já será chamada pelo evento 
                { 
                    myPath = mySaveScreen.CurrentSavePath;
                    SharePath(myPath);
                }
            }
        }
        // salva textura e compartilha a foto salva
        private void ShareTex(Texture2D screenShotTex)
        {
            mySaveScreen.SavePrintScreenTex(screenShotTex);
        }

        // compartilha imagem do caminho designado
        private void SharePath(string screenShotPath)
        {
            StartCoroutine(ShareDelay(screenShotPath));
        }

        IEnumerator ShareDelay(string screenShotPath)
        {
            yield return new WaitUntil(() => File.Exists(screenShotPath));

            Debug.Log("Verificou arquivo antes de compartilhar");

            // Utiliza o NativeShare para compartilhar  a foto
            new NativeShare().AddFile(screenShotPath)
            .SetText(ScreenShotText)
            .SetCallback((result, shareTarget) => Debug.Log($"Share result: {result}, selected app: {shareTarget}"))
            .Share();

            OnSharedCallback.Invoke();
            Debug.Log($"Compartilhou a foto do caminho ->  {screenShotPath}", this);
        }

        private void OnDisable()
        {
            // verifica o auto save e remove os listeners
            if (!autoShare) return;
            mySaveScreen.OnScreenSaved.RemoveListener(SharePath);
            if (!mySaveScreen.AutoSave) myPrintScreen.OnPrintTaken.RemoveListener(ShareTex);
        }
    }
}