using UnityEngine;

public class Shield : MonoBehaviour, IDoubleRuneAction
{
    public void TriggerDown()
    {
        Debug.Log("Shield TriggerDown!");
    }

    public void TriggerUp()
    {
        Debug.Log("Shield TriggerUp!");
    }
}
