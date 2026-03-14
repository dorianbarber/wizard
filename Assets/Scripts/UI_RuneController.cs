using UnityEngine;

public class UI_RuneController : MonoBehaviour
{
    [SerializeField] private UI_Rune runePrefab;

    public void AddRune(RuneType runeType)
    {
        UI_Rune rune = Instantiate(runePrefab, transform);
        rune.Init(runeType);
    }

    public void RemoveRune()
    {
        if (transform.childCount == 0) return;
        Destroy(transform.GetChild(0).gameObject);
    }
}
