using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserAttack : MonoBehaviour, IDoubleRuneAction
{
    [SerializeField] private float length = 10f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float originOffset = 0.5f;

    private LineRenderer lineRenderer;
    private PlayerController player;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.enabled = false;
    }

    public void TriggerDown()
    {
        player = FindFirstObjectByType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("[LaserAttack] No PlayerController found in scene.");
            return;
        }

        player.SetMovementEnabled(false);

        Vector3 origin = player.transform.position + (Vector3)(player.FacingDirection * originOffset);
        Vector3 end = origin + (Vector3)(player.FacingDirection * length);

        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, end);

        StartCoroutine(ShowLaser());
    }

    public void TriggerUp()
    {
        StopAllCoroutines();
        lineRenderer.enabled = false;
        player?.SetMovementEnabled(true);
    }

    private IEnumerator ShowLaser()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(duration);
        lineRenderer.enabled = false;
        player?.SetMovementEnabled(true);
    }
}
