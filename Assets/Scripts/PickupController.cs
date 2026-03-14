using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public UI_RuneController uiRuneController;

    private readonly Queue<RuneType> runeQueue = new();

    public void CollectRune(RuneType runeType)
    {
        runeQueue.Enqueue(runeType);
        uiRuneController.AddRune(runeType);
    }

    public void ExpendPair()
    {
        if (runeQueue.Count >= 1) runeQueue.Dequeue();
        if (runeQueue.Count >= 1) runeQueue.Dequeue();
        uiRuneController.RemovePair();
    }

    public bool TryPeekPair(out RuneType first, out RuneType second)
    {
        if (runeQueue.Count < 2)
        {
            first = default;
            second = default;
            return false;
        }

        RuneType[] arr = runeQueue.ToArray();
        first = arr[0];
        second = arr[1];
        return true;
    }
}
