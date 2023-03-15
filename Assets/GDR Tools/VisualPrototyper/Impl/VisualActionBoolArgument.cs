using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionBoolArgument : VisualActionArgument
    {
        [SerializeField] private bool boolValue = default;

        public override object GetArgumentValue()
        {
            return boolValue;
        }
    }
}
