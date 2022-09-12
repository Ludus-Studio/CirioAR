using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        xInitialPos = Screen.width * xInitialPos;
        xFinalPos = Screen.width * xFinalPos;
    }

    public AudioSource audioSource;
    public AudioClip[] sealsAudio;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            // The pos of the touch on the screen
            Vector2 vTouchPos = Input.GetTouch(0).position;

            // The ray to the touched object in the world
            Ray ray = Camera.main.ScreenPointToRay(vTouchPos);

            // Your raycast handling
            RaycastHit vHit;
            if (Physics.Raycast(ray.origin, ray.direction, out vHit))
            {
                audioSource.Stop();
                Debug.Log(vHit.transform.tag);
                if (vHit.transform.tag == "Agil")
                    audioSource.PlayOneShot(sealsAudio[0]);
                else if (vHit.transform.tag == "Digital")
                    audioSource.PlayOneShot(sealsAudio[1]);
                else if (vHit.transform.tag == "Inova")
                    audioSource.PlayOneShot(sealsAudio[2]);
            }
        }
    }



    public void GoToURL(string url)
    {
        Application.OpenURL(url);
    }


    bool sideMenu;
    public RectTransform sideMenuTransform;
    public float xInitialPos;
    public float xFinalPos;
    public Ease ease;
    public RectTransform arrow;

    public void OnSideMenu()
    {
        sideMenu = !sideMenu;



        if (sideMenu)
        {
            sideMenuTransform.DOMoveX(xFinalPos, 1).SetEase(ease);
            arrow.localScale = new Vector3(-1, 1, 1);
        }
        else if (!sideMenu)
        {
            sideMenuTransform.DOMoveX(xInitialPos, 1).SetEase(ease);
            arrow.localScale = new Vector3(1, 1, 1);
        }
    }
}
