using UnityEngine;

public class RuneController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Init(RuneType runeType)
    {
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


        Debug.Log("Yar");
        // TODO: handle player collecting rune
    }
}
