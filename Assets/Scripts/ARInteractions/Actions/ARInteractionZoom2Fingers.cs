using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludus.ARInteractions.Actions
{
    [AddComponentMenu("AR Interactions/Zoom (2 Fingers)")]
    public class ARInteractionZoom2Fingers : ARInteractionsBase
    {
        [SerializeField]
        protected float zoomThreshHolder = 0.05f;

        [Space, SerializeField]
        protected float minSize = .2f;
        [SerializeField]
        protected float maxSize = 2f;

#if UNITY_EDITOR
        protected override void Reset()
        {
            //verifica componentes que não podem ser usados juntos
        }
#endif
        protected virtual void OnEnable()
        {
            ARInteractionInputManager.DoubleTouch += Zoom;
        }

        protected virtual void Zoom(Touch touchZero, Touch touchOne)
        {
            if (myTransform == null) return;

            // posições anteriores do toque
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            // diferença entre magnetudes
            float difference = currentMagnitude - prevMagnitude;

            // escala o prefab sempre em números positivos
            myTransform.localScale += Vector3.one * difference * (zoomThreshHolder/100);
            VerifySizeBounderies();
        }

        protected void VerifySizeBounderies()
        {
            if (myTransform.localScale.x <= minSize) myTransform.localScale = Vector3.one * minSize;
            if (myTransform.localScale.x >= maxSize) myTransform.localScale = Vector3.one * maxSize;
        }

        protected virtual void OnDisable()
        {
            ARInteractionInputManager.DoubleTouch -= Zoom;
        }
    }
}

