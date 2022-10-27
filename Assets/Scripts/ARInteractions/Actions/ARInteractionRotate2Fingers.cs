using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludus.ARInteractions.Actions
{
    [AddComponentMenu("AR Interactions/Rotate (2 Fingers)")]
    public class ARInteractionRotate2Fingers : ARInteractionsBase
    {
        [SerializeField]
        private float rotateThreshHolder = 1f;
        [SerializeField]
        private RotateAxis rotateAxis;
#if UNITY_EDITOR
        protected override void Reset()
        {
            //verifica componentes que não podem ser usados juntos
        }
#endif
        void OnEnable()
        {
            ARInteractionInputManager.DoubleTouch += Rotate;
        }

        private void Rotate(Touch touchZero, Touch touchOne)
        {
            if (myTransform == null) return;

            // posições anteriores do toque
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            Vector2 prevVector = (touchOnePrevPos - touchZeroPrevPos).normalized;
            Vector2 currentVector = (touchOne.position - touchZero.position).normalized;

            // diferença entre angulos
            float angleDifference = Vector2.SignedAngle(prevVector, currentVector);

            // rotaciona o prefab
            myTransform.Rotate( rotateAxis.value * (- angleDifference * rotateThreshHolder), Space.World);
        }

        void OnDisable()
        {
            ARInteractionInputManager.DoubleTouch -= Rotate;
        }
    }
}