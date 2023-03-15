using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using D_NaughtyAttributes;

namespace D_Framework.VisualPrototyping
{
    public class VisualPrototyper : MonoBehaviour
    {
        [Expandable]
        [SerializeField] private VisualActionAbstractTrigger visualActionTrigger = null;
        [SerializeField] private List<VisualActionBlock> actionBlocks = null;

        private int triggerPressCount = 0;

        private List<VisualPrototyperSubscriber> subscribers = new List<VisualPrototyperSubscriber>();

        private void Start()
        {
            visualActionTrigger.OnVisualActionTriggered += Tick;
            visualActionTrigger.StartTracking();
        }

        private void Tick()
        {
            if (triggerPressCount < actionBlocks.Count)
            {
                actionBlocks[triggerPressCount].ExecuteActionBlock();
            }

            for (int i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].ExecuteSubscriberActionBlock(triggerPressCount);
            }

            visualActionTrigger.UpdateData(new VisualActionTimerTriggerData(EvaluateBlockDelay(triggerPressCount)));
            triggerPressCount++;
        }

        public void Subscribe(VisualPrototyperSubscriber visualPrototyperSubscriber)
        {
            subscribers.Add(visualPrototyperSubscriber);
        }

        public void SwitchTrigger(VisualActionAbstractTrigger newTrigger)
        {
            if (visualActionTrigger == newTrigger)
            {
                Debug.Log($"Attempted to set the same trigger. That is now allowed. Skipping...\n" +
                    $"Current Trigger: {visualActionTrigger.name}, New Trigger: {newTrigger.name}");

                return;
            }

            Debug.Log($"Trigger at {name} was switched from {visualActionTrigger.name} to {newTrigger.name}");

            visualActionTrigger.OnVisualActionTriggered -= Tick;
            visualActionTrigger.Dispose();
            visualActionTrigger = newTrigger;
            visualActionTrigger.OnVisualActionTriggered += Tick;
            visualActionTrigger.StartTracking();

        }

        private float EvaluateBlockDelay(int index)
        {
            float blockDelay;
            float maxDelay = 0f;

            if (index < actionBlocks.Count)
            {
                maxDelay = actionBlocks[index].MaxBlockDelay;
            }

            for (int i = 0; i < subscribers.Count; i++)
            {
                blockDelay = subscribers[i].GetMaxBlockDelay(index);
                if (blockDelay > maxDelay)
                    maxDelay = blockDelay;

            }

            return maxDelay;
        }
    }
}