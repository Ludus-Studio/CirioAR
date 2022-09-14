using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public enum TouchCountType
{
    None = 0,
    Single = 1,
    Double = 2
}

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceRaycastObjects : MonoBehaviour
{
    private ARRaycastManager m_RaycastManager;
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    private Transform prefabTransform = null;


    [SerializeField]
    private GameObject m_Prefab;
    [SerializeField]
    private float zoomThreshHolder = 0.005f;
    [SerializeField]
    private float rotateThreshHolder = 1f;

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        switch(CalculateTouch())
        {
            case TouchCountType.None:
                break;
            case TouchCountType.Single:

                if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
                    PlacePrefab(m_Hits[0]);

                break;
            case TouchCountType.Double:

                RotateAndScalePrefab();

                break;
        }
    }

    private TouchCountType CalculateTouch()
    {
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
            || Input.GetTouch(0).phase == TouchPhase.Ended)
                return TouchCountType.None;
        }

        if (Input.touchCount == 1)
            return TouchCountType.Single;

        if (Input.touchCount == 2)
            return TouchCountType.Double;

        return TouchCountType.None;
    }

    private void PlacePrefab(ARRaycastHit hit)
    {
        if(prefabTransform == null)
        {
            var prefabAux = Instantiate(m_Prefab, hit.pose.position, hit.pose.rotation);
            prefabTransform = prefabAux.transform;
        }
        else
        {
            prefabTransform.SetPositionAndRotation(hit.pose.position, hit.pose.rotation);
        }
    }

    private void RotateAndScalePrefab()
    {
        if (prefabTransform == null) return;

        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // ZOOM
        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        prefabTransform.localScale += Vector3.one * difference * zoomThreshHolder;
        if(prefabTransform.localScale.x <= 0) prefabTransform.localScale = Vector3.zero + Vector3.one * zoomThreshHolder;

        //ROTATE
        Vector2 prevVector = (touchOnePrevPos - touchZeroPrevPos).normalized;
        Vector2 currentVector = (touchOne.position - touchZero.position).normalized;

        float angleDifference = Vector2.SignedAngle(prevVector, currentVector);

        prefabTransform.Rotate(new Vector3(0, -angleDifference * rotateThreshHolder, 0), Space.World);
    }
}
