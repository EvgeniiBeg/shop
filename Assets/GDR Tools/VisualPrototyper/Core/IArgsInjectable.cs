using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public interface IArgsInjectable
    {
        public void InjectArgs(VisualActionArgs args);
    }
}