using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LinkedVisualActionArgumentTypeAttribute : Attribute
    {
        public Type linkedArgumentType;

        public LinkedVisualActionArgumentTypeAttribute(Type _linkedArgumentType)
        {
            linkedArgumentType = _linkedArgumentType;
        }
    }
}