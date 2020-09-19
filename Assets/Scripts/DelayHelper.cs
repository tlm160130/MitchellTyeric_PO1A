using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DelayHelper //running coroutine beneath the hood
{
    public static Coroutine DelayAction(this MonoBehaviour monobehaviour, Action action, float delayDuration)
    {
        return monobehaviour.StartCoroutine(DelayActionRoutine(action, delayDuration));
    }

    private static IEnumerator DelayActionRoutine(Action action, float delayDuration)//run these functions
    {
        yield return new WaitForSeconds(delayDuration);
        action();
    }
}
