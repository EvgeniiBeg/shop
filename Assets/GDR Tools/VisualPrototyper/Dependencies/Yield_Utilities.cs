using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    public static class Yield_Utilities
    {
#pragma warning disable IDE0032, IDE0044


        private static Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(100, new FloatComparer());

        private static WaitForEndOfFrame endOfFrame = null;
        public static WaitForEndOfFrame EndOfFrame
        {
            get
            {
                if (endOfFrame == null)
                {
                    endOfFrame = new WaitForEndOfFrame();
                }

                return endOfFrame;
            }
        }


        private static WaitForFixedUpdate fixedUpdate = null;
        public static WaitForFixedUpdate FixedUpdate
        {
            get
            {
                if (fixedUpdate == null)
                {
                    fixedUpdate = new WaitForFixedUpdate();
                }

                return fixedUpdate;
            }
        }

        public static WaitForSeconds Get(float seconds)
        {
            if (!timeInterval.TryGetValue(seconds, out WaitForSeconds wfs))
                timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
            return wfs;
        }

        class FloatComparer : IEqualityComparer<float>
        {
            bool IEqualityComparer<float>.Equals(float x, float y)
            {
                return x == y;
            }
            int IEqualityComparer<float>.GetHashCode(float obj)
            {
                return obj.GetHashCode();
            }
        }
#pragma warning restore IDE0032, IDE0044
    }
}