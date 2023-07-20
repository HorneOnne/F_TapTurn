using System.Collections;
using UnityEngine;


namespace TapTurn
{
    public static class Utilities
    {
        public static IEnumerator WaitAfter(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }
    }
}
