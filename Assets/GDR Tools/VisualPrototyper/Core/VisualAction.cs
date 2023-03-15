using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace D_Framework.VisualPrototyping
{
    [System.Serializable]
    public class VisualAction
    {
        [SerializeField] private string description = string.Empty; // Used for block purpose clarification when folded
        [SerializeField] private UnityEvent action = null;
        [SerializeField] private float actionDelay = 0f;

        public float ActionDelay => actionDelay;

        public void InvokeVisualAction()
        {
            action?.Invoke();
        }
    }
}