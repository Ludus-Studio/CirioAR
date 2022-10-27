using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludus.ARInteractions.Actions
{
    [AddComponentMenu("AR Interactions/Zoom (1 Finger)")]
    public class ARInteractionZoom1Finger : ARInteractionZoom2Fingers, IARInteraction1Finger
    {

#if UNITY_EDITOR
        protected override void Reset()
        {
            //verifica componentes que não podem ser usados juntos
            VerifyDeniedComponent(typeof(IARInteraction1Finger));
        }
#endif
        protected override void OnEnable()
        {
            ARInteractionInputManager.MovingTouch += Zoom;
        }
        protected void Zoom(Touch touch)
        {
            if (myTransform == null) return;

            // posição anterior do toque
            Vector2 touchPrevPos = touch.position - touch.deltaPosition;

            float pervDist = Vector3.Distance(touchPrevPos, myTransform.position);
            float currentDist = Vector3.Distance(touch.position, myTransform.position);

            // diferença entre distancia
            float difference = currentDist - pervDist;

            // escala o prefab sempre em números positivos
            myTransform.localScale += Vector3.one * difference * (zoomThreshHolder / 100);
            VerifySizeBounderies();
        }

        protected override void OnDisable()
        {
            ARInteractionInputManager.MovingTouch -= Zoom;
        }
    }
}