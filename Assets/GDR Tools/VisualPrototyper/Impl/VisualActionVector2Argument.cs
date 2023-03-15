using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionVector2Argument : VisualActionArgument
    {
        [SerializeField] private Vector2 vector2Value = default;

        public override object GetArgumentValue()
        {
            return vector2Value;
        }
    }
}
