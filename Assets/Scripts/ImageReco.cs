using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageReco : MonoBehaviour
{

    private ARTrackedImageManager _arTrackedImageManager;

    public GameObject[] infoTexts;

    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public GameObject textBox;
    GameObject trackedImg;
    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            infoTexts[0].SetActive(false);
            infoTexts[1].SetActive(true);
            
            trackedImg = trackedImage.gameObject;
            trackedImage.gameObject.SetActive(true);
        }

        foreach (var trackedImage in args.removed)
        {
            infoTexts[0].SetActive(true);
            infoTexts[1].SetActive(false);

            trackedImage.gameObject.SetActive(false);
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
    }
}
