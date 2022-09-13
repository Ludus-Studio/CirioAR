using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceRaycastObjects : MonoBehaviour
{
    private ARRaycastManager m_RaycastManager;
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    private Transform prefabTransform = null;


    [SerializeField]
    private GameObject m_Prefab;

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;
        if (Input.touchCount > 0 )
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) 
            || Input.GetTouch(0).phase == TouchPhase.Ended)
                return;
        }

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            PlacePrefab(m_Hits[0]);
        }
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
}
