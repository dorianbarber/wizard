using UnityEngine;

public class BoobyTrap : MonoBehaviour, IDoubleRuneAction
{
    public void Execute()
    {
        Debug.Log("BoobyTrap triggered!");
    }
}
