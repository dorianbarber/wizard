using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
[RequireComponent(typeof(SortingGroup))]
public class LaserAttack : MonoBehaviour, IDoubleRuneAction
{
    [SerializeField] private float length = 10f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float originOffset = 0.5f;

    private LineRenderer laserRenderer;
    private LineRenderer guidingLineRenderer;
    private PlayerController player;

    void Awake()
    {
        var sortingGroup = gameObject.GetComponent<SortingGroup>() ?? gameObject.AddComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "attack";
    }

    private void EnsureInitialized()
    {
        if (laserRenderer != null) return;

        laserRenderer = CreateLineRenderer("LaserRenderer", 0.05f, new Color(1f, 0f, 0f, 0.9f));
        guidingLineRenderer = CreateLineRenderer("GuidingLineRenderer", 0.05f, new Color(1f, 0f, 0f, 0.2f));
    }

    private LineRenderer CreateLineRenderer(string goName, float width, Color color)
    {
        var go = new GameObject(goName);
        go.transform.SetParent(transform);
        var lr = go.AddComponent<LineRenderer>();
        SetupLine(lr, width, color);
        lr.enabled = false;
        return lr;
    }

    private static void SetupLine(LineRenderer lr, float width, Color color)
    {
        lr.positionCount = 2;
        lr.startWidth = width;
        lr.endWidth = width;
        lr.startColor = color;
        lr.endColor = color;
    }

    public void TriggerDown()
    {
        EnsureInitialized();
        player = FindFirstObjectByType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("[LaserAttack] No PlayerController found in scene.");
            return;
        }

        player.SetMovementEnabled(false);
        StartCoroutine(ShowGuidingLaser());
    }

    public void TriggerUp()
    {
        if (player == null) return;

        StopAllCoroutines();
        guidingLineRenderer.enabled = false;
        player.SetMovementEnabled(true);
        StartCoroutine(FireLaser(player.FacingDirection));
    }

    private IEnumerator ShowGuidingLaser()
    {
        guidingLineRenderer.enabled = true;
        while (true)
        {
            UpdateLine(guidingLineRenderer, player.FacingDirection);
            yield return null;
        }
    }

    private IEnumerator FireLaser(Vector2 direction)
    {
        UpdateLine(laserRenderer, direction);
        laserRenderer.enabled = true;
        yield return new WaitForSeconds(duration);
        laserRenderer.enabled = false;
    }

    private void UpdateLine(LineRenderer lr, Vector2 direction)
    {
        Vector3 origin = player.transform.position + (Vector3)(direction * originOffset);
        Vector3 end = origin + (Vector3)(direction * length);
        lr.SetPosition(0, origin);
        lr.SetPosition(1, end);
    }
}
