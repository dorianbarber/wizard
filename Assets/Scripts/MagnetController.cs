using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class MagnetController : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float pullStrength = 10f;

    private CircleCollider2D circleCollider;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        circleCollider.radius = radius;
    }

    void OnValidate()
    {
        if (circleCollider == null)
            circleCollider = GetComponent<CircleCollider2D>();

        if (circleCollider != null)
            circleCollider.radius = radius;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        Vector2 direction = (Vector2)(transform.position - other.transform.position);
        float distance = direction.magnitude;

        if (distance > 0f)
        {
            float force = pullStrength / distance;
            rb.AddForce(direction.normalized * force);
        }
    }
}
