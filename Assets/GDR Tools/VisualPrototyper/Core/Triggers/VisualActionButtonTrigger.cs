using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    [CreateAssetMenu(fileName = "New Visual Action Button Trigger", menuName = "Custom Objects/Visual Prototyping/Button Trigger")]
    public class VisualActionButtonTrigger : VisualActionAbstractTrigger
    {
        [SerializeField] private KeyCode triggerButtonCode = KeyCode.None;

        private Coroutine trackRoutine;

        public override void StartTracking()
        {
            trackRoutine = MB_Utilities.ExecuteWhen(() => 
                Input.GetKeyDown(triggerButtonCode), InvokeVisualActionEvent
            );
        }

        public override void Dispose()
        {
            if (MB_Utilities.Instance == null)
                return;

            MB_Utilities.StopHostCoroutine(trackRoutine);
        }

        public override void UpdateData(VisualActionTriggerData data) { }
    }
}