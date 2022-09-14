using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

// tipo de toque
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
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();// guarda o hit com o plano calculado
    private Transform prefabTransform = null;// guarda o objeto instanciado


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
            // toque nulo não faz nada
            case TouchCountType.None:
                break;
            // toque de 1 dedo ajusta posição de objeto
            case TouchCountType.Single:

                if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
                    PlacePrefab(m_Hits[0]);

                break;
            // toque de 2 dedos escala e rotaciona objeto
            case TouchCountType.Double:

                RotateAndScalePrefab();

                break;
        }
    }

    // calcula qual o tipo de toque na tela
    private TouchCountType CalculateTouch()
    {
        // verifica se o toque estão dentro da UI
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
            || Input.GetTouch(0).phase == TouchPhase.Ended)
                return TouchCountType.None;
        }

        // toque de apenas 1 dedo
        if (Input.touchCount == 1)
            return TouchCountType.Single;

        // toque de 2 dedos
        if (Input.touchCount == 2)
            return TouchCountType.Double;

        // quantidade de toque menor ou igual a 0 ou maior que 2
        return TouchCountType.None;
    }

    private void PlacePrefab(ARRaycastHit hit)
    {
        // caso objeto for nulo instancia um novo na posição correta
        if(prefabTransform == null)
        {
            var prefabAux = Instantiate(m_Prefab, hit.pose.position, hit.pose.rotation);
            prefabTransform = prefabAux.transform;
        }
        // caso já exista reposiciona objeto
        else
        {
            prefabTransform.position = hit.pose.position;
        }
    }

    // rotaciona e escala objeto
    private void RotateAndScalePrefab()
    {
        if (prefabTransform == null) return;

        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // posições anteriores do toque
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // -------- ZOOM -------------
        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        // diferença entre magnetudes
        float difference = currentMagnitude - prevMagnitude;

        // escala o prefab sempre em números positivos
        prefabTransform.localScale += Vector3.one * difference * zoomThreshHolder;
        if(prefabTransform.localScale.x <= 0) prefabTransform.localScale = Vector3.zero + Vector3.one * zoomThreshHolder;

        //------------- ROTATE -------------
        Vector2 prevVector = (touchOnePrevPos - touchZeroPrevPos).normalized;
        Vector2 currentVector = (touchOne.position - touchZero.position).normalized;

        // diferença entre angulos
        float angleDifference = Vector2.SignedAngle(prevVector, currentVector);

        // rotaciona o prefab
        prefabTransform.Rotate(new Vector3(0, -angleDifference * rotateThreshHolder, 0), Space.World);
    }
}
