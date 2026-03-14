using UnityEngine;

public class SlowField : MonoBehaviour, IDoubleRuneAction
{
    public void TriggerDown()
    {
        Debug.Log("SlowField TriggerDown!");
    }

    public void TriggerUp()
    {
        Debug.Log("SlowField TriggerUp!");
    }
}
