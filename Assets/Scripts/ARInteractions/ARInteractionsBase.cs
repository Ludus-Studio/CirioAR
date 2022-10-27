using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludus.ARInteractions
{
    public class ARInteractionsBase : MonoBehaviour, IARInteractionComponent
    {
        [Tooltip("Objeto a ser movido.\nCaso deixe nulo o script irá mover seu próprio GameObject")]
        [SerializeField]
        protected GameObject _prefab;

        protected GameObject currentGO;
        protected Transform myTransform;


        public GameObject Prefab => currentGO;

        protected virtual void Awake()
        {
            if (_prefab == null)
                SetGameObject(this.gameObject);
            else
                SetGameObject(_prefab);
        }

        public void SetGameObject(GameObject go)
        {
            currentGO = go;
            myTransform = currentGO.transform;
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            
        }

        protected void VerifyDeniedComponent(System.Type comp) 
        {
            var component = gameObject.GetComponents(comp);
            if (component != null && component.Length > 1)
            {
                System.Type myType = this.GetType();
                Debug.LogError($"O componente {myType.Name} não pode ser inserido junto com o componente {comp.Name}");

                DestroyImmediate(GetComponent(myType));
            }
        }
#endif
    }
}