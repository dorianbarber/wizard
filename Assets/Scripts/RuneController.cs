using UnityEngine;

public class RuneController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private RuneType runeType;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        RuneType[] values = (RuneType[])System.Enum.GetValues(typeof(RuneType));
        runeType = values[Random.Range(0, values.Length)];

        spriteRenderer.color = runeType switch
        {
            RuneType.Red => Color.red,
            RuneType.Green => Color.green,
            RuneType.Blue => Color.blue,
            _ => Color.white
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 10) return;

        PickupController pickupController = other.GetComponent<PickupController>();
        pickupController.CollectRune(runeType);
        Destroy(gameObject);
    }
}
