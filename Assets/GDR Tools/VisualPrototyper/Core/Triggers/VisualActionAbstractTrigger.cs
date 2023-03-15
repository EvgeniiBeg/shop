using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public abstract class VisualActionAbstractTrigger : ScriptableObject
    {
        public delegate void OnVisualActionTriggeredEventHandler();
        public event OnVisualActionTriggeredEventHandler OnVisualActionTriggered;

        public abstract void StartTracking();
        public abstract void Dispose();
        public abstract void UpdateData(VisualActionTriggerData data);

        protected void InvokeVisualActionEvent()
        {
            OnVisualActionTriggered?.Invoke();
        }
    }
}