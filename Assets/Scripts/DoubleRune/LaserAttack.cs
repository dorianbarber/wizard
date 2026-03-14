using UnityEngine;

public class LaserAttack : MonoBehaviour, IDoubleRuneAction
{
    public void Execute()
    {
        Debug.Log("LaserAttack triggered!");
    }
}
