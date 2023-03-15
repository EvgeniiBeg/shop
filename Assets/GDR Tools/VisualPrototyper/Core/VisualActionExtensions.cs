using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public static class VisualActionExtensions 
    {
        public static Type GetLinkedArgumentType(this VisualActionArgType visualActionArgType)
        {
            FieldInfo fi = visualActionArgType.GetType().GetField(visualActionArgType.ToString());
            LinkedVisualActionArgumentTypeAttribute[] attributes = fi.GetCustomAttributes(typeof(LinkedVisualActionArgumentTypeAttribute), false) 
                as LinkedVisualActionArgumentTypeAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().linkedArgumentType;
            }

            return null;
        }
    }
}