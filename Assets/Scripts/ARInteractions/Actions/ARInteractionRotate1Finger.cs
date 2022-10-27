using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludus.ARInteractions.Actions
{
    [AddComponentMenu("AR Interactions/Rotate (1 Finger)")]
    public class ARInteractionRotate1Finger : ARInteractionsBase, IARInteraction1Finger
    {
        [SerializeField]
        private float rotateThreshHolder = .5f;
        
        [Space,SerializeField]
        private RotateAxis rotateVerticalAxis = new RotateAxis(true, false, false);
        [SerializeField]
        private RotateAxis rotateHorizontalAxis = new RotateAxis(false, true, false);
#if UNITY_EDITOR
        protected override void Reset()
        {
            //verifica componentes que não podem ser usados juntos
            VerifyDeniedComponent(typeof(IARInteraction1Finger));
        }
#endif
        void OnEnable()
        {
            ARInteractionInputManager.MovingTouch += Rotate;
        }

        private void Rotate(Touch touch)
        {
            if (myTransform == null) return;

            // posição anterior do toque
            Vector2 touchPrevPos = touch.position - touch.deltaPosition;


            Vector2 difVector = (touch.position - touchPrevPos);

            Vector3 verticalVector = rotateVerticalAxis.value * difVector.y;
            Vector3 horizontalVector = rotateHorizontalAxis.value * -difVector.x;
            // rotaciona o prefab
            myTransform.Rotate((verticalVector + horizontalVector) * rotateThreshHolder, Space.World);
        }

        void OnDisable()
        {
            ARInteractionInputManager.MovingTouch -= Rotate;
        }
    }
}