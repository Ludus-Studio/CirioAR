using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Ludus.ARInteractions
{
    [AddComponentMenu("AR Interactions/AR Interaction Manager")]
    public class ARInteractionInputManager : MonoBehaviour
    {
        public static ARInteractionInputManager Instance;

        public static Action<Touch> SingleTouch;
        public static Action<Touch> MovingTouch;
        public static Action<Touch, Touch > DoubleTouch;
        public static Action NoneTouch;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        private void Update()
        {
            switch (CalculateTouch())
            {
                // toque nulo não faz nada
                case TouchType.None:
                    NoneTouch?.Invoke();
                    break;
                // toque de 1 dedo ajusta posição de objeto
                case TouchType.Single:
                    SingleTouch?.Invoke(Input.GetTouch(0));
                    break;
                // toque movendo
                case TouchType.Move:
                    MovingTouch?.Invoke(Input.GetTouch(0)); ;
                    break;
                // toque de 2 dedos escala e rotaciona objeto
                case TouchType.Double:
                    DoubleTouch?.Invoke(Input.GetTouch(0), Input.GetTouch(1));
                    break;
                
            }
        }

        // calcula qual o tipo de toque na tela
        private TouchType CalculateTouch()
        {
            // verifica se o toque estão dentro da UI
            if (Input.touchCount > 0)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
                || Input.GetTouch(0).phase == TouchPhase.Ended)
                    return TouchType.None;
            }

            // toque de apenas 1 dedo
            if (Input.touchCount == 1)
            {
                // toque movendo pela tela
                if(Input.GetTouch(0).phase == TouchPhase.Moved)
                    return TouchType.Move;

                return TouchType.Single;
            }

            // toque de 2 dedos
            if (Input.touchCount == 2)
                return TouchType.Double;

            // quantidade de toque menor ou igual a 0 ou maior que 2
            return TouchType.None;
        }
    }
}