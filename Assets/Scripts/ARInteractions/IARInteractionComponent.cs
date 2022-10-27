using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludus.ARInteractions
{
    public interface IARInteractionComponent 
    {
        public void SetGameObject(GameObject go);
    }
    
    public interface IARInteraction1Finger { }
}