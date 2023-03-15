using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using D_NaughtyAttributes;

namespace D_Framework.VisualPrototyping
{
    [CreateAssetMenu(fileName = "New Visual Action Arguments", menuName = "Custom Objects/Visual Prototyping/Arguments")]
    public class VisualActionArgs : ScriptableObject
    {
        [SerializeField] [Expandable] 
        private List<VisualActionArgument> args = null;

        public T GetArgumentValueAs<T>(int index)
        {
            return (T)args[index].GetArgumentValue();
        }
    }

}