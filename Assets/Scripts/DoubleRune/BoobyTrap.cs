using UnityEngine;

public class BoobyTrap : MonoBehaviour, IDoubleRuneAction
{
    public void TriggerDown()
    {
        Debug.Log("BoobyTrap TriggerDown!");
    }

    public void TriggerUp()
    {
        Debug.Log("BoobyTrap TriggerUp!");
    }
}
