using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public enum VisualActionArgType
    {
        [LinkedVisualActionArgumentType(typeof(VisualActionIntArgument))]
        Int,

        [LinkedVisualActionArgumentType(typeof(VisualActionFloatArgument))]
        Float,

        [LinkedVisualActionArgumentType(typeof(VisualActionBoolArgument))]
        Bool,

        [LinkedVisualActionArgumentType(typeof(VisualActionStringArgument))]
        String,

        [LinkedVisualActionArgumentType(typeof(VisualActionVector3Argument))]
        Vector3,

        [LinkedVisualActionArgumentType(typeof(VisualActionVector2Argument))]
        Vector2,

        [LinkedVisualActionArgumentType(typeof(VisualActionColorArgument))]
        Color
    }
}