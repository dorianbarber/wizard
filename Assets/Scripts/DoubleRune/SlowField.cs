using UnityEngine;

public class SlowField : MonoBehaviour, IDoubleRuneAction
{
    public void Execute()
    {
        Debug.Log("SlowField triggered!");
    }
}
