using UnityEngine;
using UnityEngine.UI;



public class UI_Rune : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private Image background;


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

    public RuneType GetRuneType() => type;

    public void SetText(string value)
    {
        text.text = value;
        background.enabled = true;
    }

    public void ClearText()
    {
        text.text = string.Empty;
        background.enabled = false;
    }
}
