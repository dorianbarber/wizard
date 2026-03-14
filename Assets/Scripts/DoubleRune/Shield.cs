using UnityEngine;

public class Shield : MonoBehaviour, IDoubleRuneAction
{
    public void Execute()
    {
        Debug.Log("Shield triggered!");
    }
}
