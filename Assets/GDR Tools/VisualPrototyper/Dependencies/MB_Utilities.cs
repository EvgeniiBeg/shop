using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    public class MB_Utilities : Singleton<MB_Utilities>
    {
        private const int MAX_ROUTINE_COUNT = 1_000;
        private const int EMERGENCY_ITERATION_COUNT = 75;

        private static Dictionary<int, Coroutine> coroutineDictionary = new Dictionary<int, Coroutine>();

        public static Coroutine StartHostCoroutine(IEnumerator routine)
        {
            return Instance.StartCoroutine(routine);
        }

        public static void StopHostCoroutine(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }

        public static void StartHostCoroutine(IEnumerator routine, out int coroutineId)
        {
            for (int i = 0; i < MAX_ROUTINE_COUNT; i++)
            {
                if (!coroutineDictionary.TryGetValue(i, out _))
                {
                    Debug.Log($"Started coroutine with id {i}");
                    coroutineId = i;
                    coroutineDictionary.Add(i, Instance.StartCoroutine(routine));
                    return;
                }
            }

            coroutineId = -1;
        }

        public static void StopHostCoroutine(int id)
        {
            if (coroutineDictionary.TryGetValue(id, out Coroutine coroutine))
            {
                Debug.Log($"Stopped coroutine with id {id}");
                Instance.StopCoroutine(coroutine);
                coroutineDictionary.Remove(id);
            }
        }

        public static Coroutine ExecuteWhen(System.Func<bool> condition, System.Action action)
        {
            return Instance.StartCoroutine(Instance.ExecuteWhenRoutine(condition, action));
        }

        private int emergency = 0;

        private IEnumerator ExecuteWhenRoutine(System.Func<bool> condition, System.Action action)
        {
            emergency++;

            if (emergency >= EMERGENCY_ITERATION_COUNT)
            {
                Debug.LogError("Emergency");
                yield break;
            }


            while (!condition())
            {
                yield return null;
            }

            action();

            yield return Yield_Utilities.EndOfFrame;
            yield return ExecuteWhenRoutine(condition, action);
        }

        public static Coroutine Execute(float delay, System.Action coroutineAction)
        {
            return Instance.StartCoroutine(Instance.ExecuteRoutine(delay, coroutineAction));
        }

        private IEnumerator ExecuteRoutine(float delay, System.Action coroutineAction)
        {
            yield return new WaitForSeconds(delay);
            coroutineAction();
        }
    }
}