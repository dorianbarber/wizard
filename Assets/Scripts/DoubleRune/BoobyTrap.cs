using UnityEngine;

public class BoobyTrap : MonoBehaviour, IDoubleRuneAction
{
    public void TriggerDown(PlayerController player)
    {
        Debug.Log("BoobyTrap TriggerDown!");
    }

    public void TriggerUp()
    {
        Debug.Log("BoobyTrap TriggerUp!");
    }
}
