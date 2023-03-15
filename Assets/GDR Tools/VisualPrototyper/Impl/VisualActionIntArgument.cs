using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionIntArgument : VisualActionArgument
    {
        [SerializeField] private int intValue = default;

        public override object GetArgumentValue()
        {
            return intValue;
        }
    }
}