using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionArgumentFactory : IFactory<VisualActionArgument>
    {
        public VisualActionArgument GetElement(Enum visualArgumentType)
        {
            VisualActionArgType castedArgumentType = (VisualActionArgType)visualArgumentType;
            return ScriptableObject.CreateInstance(castedArgumentType.GetLinkedArgumentType()) as VisualActionArgument;
        }

    }
}