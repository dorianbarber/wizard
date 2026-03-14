using UnityEngine;

public class UI_RuneController : MonoBehaviour
{
    [SerializeField] private UI_Rune runePrefab;

    public void AddRune(RuneType runeType)
    {
        UI_Rune rune = Instantiate(runePrefab, transform);
        rune.Init(runeType);
        RefreshLabels();
    }

    public void RemoveRune()
    {
        if (transform.childCount == 0) return;
        Transform child = transform.GetChild(0);
        child.SetParent(null);
        Destroy(child.gameObject);
        RefreshLabels();
    }

    public void RemovePair()
    {
        for (int i = 0; i < 2 && transform.childCount > 0; i++)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        RefreshLabels();
    }

    private void RefreshLabels()
    {
        int count = transform.childCount;

        for (int i = 0; i + 1 < count; i += 2)
        {
            UI_Rune a = transform.GetChild(i).GetComponent<UI_Rune>();
            UI_Rune b = transform.GetChild(i + 1).GetComponent<UI_Rune>();

            RuneType typeA = a.GetRuneType();
            RuneType typeB = b.GetRuneType();

            ActionType action = RuneToAction.GetAction(typeA, typeB);
            a.SetText(RuneToAction.GetActionName(action));
            b.ClearText();
        }

        // Clear label on singlet if odd number
        if (count % 2 != 0)
            transform.GetChild(count - 1).GetComponent<UI_Rune>().ClearText();
    }
}
