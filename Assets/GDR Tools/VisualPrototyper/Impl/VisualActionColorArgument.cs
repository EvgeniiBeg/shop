using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionColorArgument : VisualActionArgument
    {
        [SerializeField] private Color colorValue = default;

        public override object GetArgumentValue()
        {
            return colorValue;
        }
    }
}