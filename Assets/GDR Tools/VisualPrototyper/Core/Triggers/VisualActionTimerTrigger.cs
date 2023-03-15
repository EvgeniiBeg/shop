using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    [CreateAssetMenu(fileName = "New Visual Action Timer Trigger", menuName = "Custom Objects/Visual Prototyping/Timer Trigger")]
    public class VisualActionTimerTrigger : VisualActionAbstractTrigger
    {
        [SerializeField] private float tickDuration = 1f;

        private float blockDelay;
        //private Coroutine trackRoutine;
        private int trackRoutineId;

        public override void StartTracking()
        {
            //trackRoutine = MB_Utilities.StartHostCoroutine(TickRoutine());
            MB_Utilities.StartHostCoroutine(TickRoutine(), out trackRoutineId);
        }

        public override void Dispose()
        {
            if (MB_Utilities.Instance == null)
                return;

            MB_Utilities.StopHostCoroutine(trackRoutineId);
           // MB_Utilities.StopHostCoroutine(trackRoutine);
        }

        public override void UpdateData(VisualActionTriggerData data)
        {
            VisualActionTimerTriggerData timerTriggerData = data as VisualActionTimerTriggerData;
            blockDelay = timerTriggerData.blockDelay;
        }

        private IEnumerator TickRoutine()
        {
            yield return new WaitForSeconds(blockDelay + tickDuration);
            InvokeVisualActionEvent();

            yield return TickRoutine();
        }
    }
}