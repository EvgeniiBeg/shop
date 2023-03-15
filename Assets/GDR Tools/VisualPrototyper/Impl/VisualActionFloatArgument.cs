using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionFloatArgument : VisualActionArgument
    {
        [SerializeField] private float floatValue = default;

        public override object GetArgumentValue()
        {
            return floatValue;
        }
    }
}
