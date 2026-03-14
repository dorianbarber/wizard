using UnityEngine;
using UnityEngine.UI;

public enum RuneType
{
    Red,
    Green,
    Blue
}

public class UI_Rune : MonoBehaviour
{
    [SerializeField] private Image image;

    private RuneType type;

    private static Color GetColor(RuneType runeType) => runeType switch
    {
        RuneType.Red => Color.red,
        RuneType.Green => Color.green,
        RuneType.Blue => Color.blue,
        _ => Color.white
    };

    public void Init(RuneType runeType)
    {
        type = runeType;
        image.color = GetColor(runeType);
    }
}
