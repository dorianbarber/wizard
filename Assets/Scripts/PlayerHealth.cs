using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void Hit()
    {
        Destroy(gameObject);
    }
}
