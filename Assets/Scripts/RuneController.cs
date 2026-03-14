using UnityEngine;

public enum ActionType
{
    Basic,
    Laser,
    Shield,
    Slow,
    Trap,
    Magnet,
    Double
}

public enum RuneType
{
    Red,
    Green,
    Blue
}

public class RuneToAction : MonoBehaviour
{

    public static ActionType GetAction(RuneType rune)
    {
        return ActionType.Basic;
    }

    public static string GetActionName(ActionType action) => action switch
    {
        ActionType.Basic => "Basic",
        ActionType.Laser => "Laser",
        ActionType.Shield => "Shield",
        ActionType.Slow => "Slow",
        ActionType.Trap => "Trap",
        ActionType.Magnet => "Magnet",
        ActionType.Double => "Double",
        _ => "Unknown"
    };

    public static ActionType GetAction(RuneType rune1, RuneType rune2)
    {
        // Normalize order so (A, B) == (B, A)
        if (rune1 > rune2) (rune1, rune2) = (rune2, rune1);

        return (rune1, rune2) switch
        {
            (RuneType.Red, RuneType.Red) => ActionType.Laser,
            (RuneType.Green, RuneType.Green) => ActionType.Shield,
            (RuneType.Blue, RuneType.Blue) => ActionType.Slow,
            (RuneType.Red, RuneType.Green) => ActionType.Trap,
            (RuneType.Red, RuneType.Blue) => ActionType.Magnet,
            (RuneType.Green, RuneType.Blue) => ActionType.Double,
            _ => ActionType.Basic
        };
    }
}

public class RuneController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private RuneType runeType;
    private bool collected;

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
        if (collected) return;
        if (other.gameObject.layer != 10) return;

        collected = true;
        PickupController pickupController = other.GetComponent<PickupController>();
        pickupController.CollectRune(runeType);
        Destroy(gameObject);
    }
}
