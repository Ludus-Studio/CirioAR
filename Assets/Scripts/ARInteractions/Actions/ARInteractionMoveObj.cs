using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Ludus.ARInteractions.Actions
{
    [AddComponentMenu("AR Interactions/Move Object (1 Finger)")]
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARInteractionMoveObj : ARInteractionsBase, IARInteraction1Finger
    {
        private ARRaycastManager m_RaycastManager;
        private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();// guarda o hit com o plano calculado

        protected override void Awake()
        {
            base.Awake();
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

#if UNITY_EDITOR
        protected override void Reset()
        {
            //verifica componentes que não podem ser usados juntos
            VerifyDeniedComponent(typeof(IARInteraction1Finger)); 
        }
#endif
        void OnEnable()
        {
            ARInteractionInputManager.MovingTouch += Move;
        }

        private void Move(Touch touch)
        {
            if (myTransform == null) return;
            if (m_RaycastManager.Raycast(touch.position, m_Hits))
                myTransform.position = m_Hits[0].pose.position;
        }

        void OnDisable()
        {
            ARInteractionInputManager.MovingTouch -= Move;
        }
    }
}