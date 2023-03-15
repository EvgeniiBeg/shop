using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualPrototyperSubscriber : MonoBehaviour
    {
        [SerializeField] private List<VisualActionBlock> visualActions = null;

        private void Awake()
        {
            VisualPrototyper visualPrototyper = FindObjectOfType<VisualPrototyper>();

            if (visualPrototyper != null)
            {
                visualPrototyper.Subscribe(this);
            }
        }

        public void ExecuteSubscriberActionBlock(int index)
        {
            if (index >= visualActions.Count)
                return;

            visualActions[index].ExecuteActionBlock();
        }

        public float GetMaxBlockDelay(int index)
        {
            if (index < visualActions.Count)
            {
                return visualActions[index].MaxBlockDelay;
            }

            return 0f;
        }
    }
}