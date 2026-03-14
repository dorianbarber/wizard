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
}
