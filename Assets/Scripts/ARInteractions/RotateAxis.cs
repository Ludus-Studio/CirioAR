using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ludus.ARInteractions
{
    [System.Serializable]
    public class RotateAxis
    {
        public bool x, y, z;

        public Vector3 value => SetVector();
        public RotateAxis()
        {
            SetValues(false, true, false);
        }
        public RotateAxis(bool x, bool y, bool z)
        {
            SetValues(x, y, z);
        }

        private void SetValues(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        private Vector3 SetVector()
        {
            return new Vector3(x ? 1f : 0f, y ? 1f : 0f, z ? 1f : 0f);
        }
    }
}