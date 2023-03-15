using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    public class VisualActionTimerTriggerData : VisualActionTriggerData
    {
        public float blockDelay;

        public VisualActionTimerTriggerData(float _blockDelay)
        {
            blockDelay = _blockDelay;
        }
    }
}