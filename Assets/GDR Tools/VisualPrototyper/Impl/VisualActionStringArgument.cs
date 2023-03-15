using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionStringArgument : VisualActionArgument
    {
        [SerializeField] private string stringValue = default;

        public override object GetArgumentValue()
        {
            return stringValue;
        }
    }
}
