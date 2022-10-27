using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Ludus.ARInteractions.Actions
{
    [AddComponentMenu("AR Interactions/Place Object (click)")]
    public class ARInteractionPlacePrefab : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Prefab;
        [SerializeField]
        private float m_PrefabSize = 1f;

        [Space, Tooltip("Adiciona o prefab criado automaticamente em todos os componentes ARInteractions no objeto")]
        [SerializeField]
        private bool AutoSetPrefabs = true;
        [Tooltip("(Requer o componente TogglePlaneVisualizer)\nDesativa o visualizador de plano quando o objeto é colocado")]
        [SerializeField]
        private bool disablePlaneWhenPlace = false;


        private ARRaycastManager m_RaycastManager;
        private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();// guarda o hit com o plano calculado
        private Transform prefabTransform = null;// guarda o objeto instanciado
        private TogglePlaneVisualizer planerVisualizer;

        private void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            planerVisualizer = GetComponent<TogglePlaneVisualizer>();
        }

        private void OnEnable()
        {
            ARInteractionInputManager.SingleTouch += PlacePrefab;
        }

        // caso objeto for nulo instancia um novo na posição correta
        private void PlacePrefab(Touch touch)
        {
            if (prefabTransform != null) return;

            if (m_RaycastManager.Raycast(touch.position, m_Hits))
            {
                var prefabAux = Instantiate(m_Prefab, m_Hits[0].pose.position, m_Hits[0].pose.rotation);
                prefabTransform = prefabAux.transform;
                prefabTransform.localScale = Vector3.one * m_PrefabSize;
                SetObjectToComponents(prefabAux);
                if(disablePlaneWhenPlace) SetPlaneVisualizer(false);
            }
        }

        private void SetObjectToComponents(GameObject prefab)
        {
            if (!AutoSetPrefabs) return;
            var comps = GetComponents<IARInteractionComponent>();
            foreach (var comp in comps)
            {
                comp.SetGameObject(prefab);
            }
        }

        private void SetPlaneVisualizer(bool val)
        {
            if (planerVisualizer == null) return;
            planerVisualizer.SetPlaneVisualizer(val);
        }

        [ContextMenu("Remove Prefab")]
        public void RemovePrefab()
        {
            if (prefabTransform == null) return;
            Destroy( prefabTransform.gameObject);
            SetPlaneVisualizer(true);
        }

        private void OnDisable()
        {
            ARInteractionInputManager.SingleTouch -= PlacePrefab;
        }
    }
}