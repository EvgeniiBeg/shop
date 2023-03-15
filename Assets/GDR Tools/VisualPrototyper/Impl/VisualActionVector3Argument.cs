using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionVector3Argument : VisualActionArgument
    {
        [SerializeField] private Vector3 vector3Value = default;

        public override object GetArgumentValue()
        {
            return vector3Value;
        }
    }
}
