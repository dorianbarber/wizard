using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; } = 10f;
    public Vector2 Direction { get; set; }
    public GameObject Shooter { get; set; }

    void Update()
    {
        transform.Translate((Vector3)Direction * (Speed * Time.deltaTime), Space.World);

        Vector3 vp = Camera.main.WorldToViewportPoint(transform.position);
        if (vp.x < 0 || vp.x > 1 || vp.y < 0 || vp.y > 1)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Shooter) return;
        var health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Hit();
            Destroy(gameObject);
        }
    }
}
