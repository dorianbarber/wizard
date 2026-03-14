using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class Shield : MonoBehaviour, IDoubleRuneAction
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private int segments = 64;
    [SerializeField] private float lineWidth = 0.05f;
    [SerializeField] private Color shieldColor = new Color(0.3f, 0.6f, 1f, 0.35f);

    private LineRenderer shieldRenderer;
    private PlayerController player;

    void Awake()
    {
        var sortingGroup = gameObject.GetComponent<SortingGroup>() ?? gameObject.AddComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "attack";
    }

    private void EnsureInitialized()
    {
        if (shieldRenderer != null) return;

        var go = new GameObject("ShieldRenderer");
        go.transform.SetParent(transform);
        shieldRenderer = go.AddComponent<LineRenderer>();
        shieldRenderer.positionCount = segments + 1;
        shieldRenderer.startWidth = lineWidth;
        shieldRenderer.endWidth = lineWidth;
        shieldRenderer.startColor = shieldColor;
        shieldRenderer.endColor = shieldColor;
        shieldRenderer.loop = true;
        shieldRenderer.useWorldSpace = true;
        shieldRenderer.enabled = false;
    }

    public void TriggerDown()
    {
        EnsureInitialized();
        player = FindFirstObjectByType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("[Shield] No PlayerController found in scene.");
            return;
        }

        shieldRenderer.enabled = true;
        StartCoroutine(TrackShield());
    }

    public void TriggerUp()
    {
        StopAllCoroutines();
        if (shieldRenderer != null)
            shieldRenderer.enabled = false;
    }

    private IEnumerator TrackShield()
    {
        while (true)
        {
            UpdateCircle(player.transform.position);
            yield return null;
        }
    }

    private void UpdateCircle(Vector3 center)
    {
        for (int i = 0; i <= segments; i++)
        {
            float angle = 2f * Mathf.PI * i / segments;
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y + radius * Mathf.Sin(angle);
            shieldRenderer.SetPosition(i, new Vector3(x, y, center.z));
        }
    }
}
