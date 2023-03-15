using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.VisualPrototyping
{
    [System.Serializable]
    public class VisualActionBlock
    {
        [SerializeField] private List<VisualAction> actionBlock = new List<VisualAction>();

        private int currentActionIndex = 0;

        public float MaxBlockDelay
        {
            get
            {
                float delay = 0;

                for (int i = 0; i < actionBlock.Count; i++)
                {
                    if (actionBlock[i].ActionDelay > delay)
                        delay = actionBlock[i].ActionDelay;
                }

                return delay;
            }
        }

        public void ExecuteActionBlock()
        {
            VisualActionBlockDelaySearchData useRoutine = DetermineRoutineUse();

            if (useRoutine.hasDelay)
            {
                for (int i = 0; i < useRoutine.delayActionIndex; i++)
                {
                    actionBlock[i].InvokeVisualAction();
                }

                currentActionIndex = useRoutine.delayActionIndex;
                MB_Utilities.StartHostCoroutine(ExecuteActionBlockRoutine());
            }
            else
            {
                for (int i = 0; i < actionBlock.Count; i++)
                {
                    actionBlock[i].InvokeVisualAction();
                }
            }
        }

        private IEnumerator ExecuteActionBlockRoutine()
        {
            if (currentActionIndex >= actionBlock.Count)
                yield break;

            VisualAction currentAction = actionBlock[currentActionIndex];
            yield return new WaitForSeconds(currentAction.ActionDelay);
           
            currentAction.InvokeVisualAction();
            currentActionIndex++;

            yield return ExecuteActionBlockRoutine();
        }

        private VisualActionBlockDelaySearchData DetermineRoutineUse()
        {
            for (int i = 0; i < actionBlock.Count; i++)
            {
                if (actionBlock[i].ActionDelay != 0)
                    return new VisualActionBlockDelaySearchData(true, i);
            }

            return new VisualActionBlockDelaySearchData(false, -1);
        }

        private struct VisualActionBlockDelaySearchData
        {
            public bool hasDelay;
            public int delayActionIndex;

            public VisualActionBlockDelaySearchData(bool _hasDelay, int _index)
            {
                hasDelay = _hasDelay;
                delayActionIndex = _index;
            }
        }
    }
}