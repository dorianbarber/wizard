using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SingleRuneActionController : MonoBehaviour
{
    [SerializeField] private PickupController pickupController;
    private PlayerController playerController;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float idleLineLength = 0.4f;
    [SerializeField] private float aimedLineLength = 3f;

    private static readonly Color IdleColor = new(1f, 1f, 0f, 0.25f);
    private static readonly Color AimedColor = new(1f, 1f, 0f, 0.9f);

    private LineRenderer guidingLineRenderer;
    private bool isCharged = false;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        var go = new GameObject("SingleRuneGuidingLine");
        go.transform.SetParent(transform);
        guidingLineRenderer = go.AddComponent<LineRenderer>();
        guidingLineRenderer.positionCount = 2;
        guidingLineRenderer.startWidth = 0.05f;
        guidingLineRenderer.endWidth = 0.05f;
        guidingLineRenderer.startColor = IdleColor;
        guidingLineRenderer.endColor = IdleColor;
    }

    void Update()
    {
        var gamepad = playerController.assignedGamepad;
        if (gamepad == null) return;

        if (gamepad.rightShoulder.wasPressedThisFrame && pickupController.HasOne())
        {
            isCharged = true;
            guidingLineRenderer.startColor = AimedColor;
            guidingLineRenderer.endColor = AimedColor;
        }

        if (isCharged && gamepad.rightShoulder.wasReleasedThisFrame)
        {
            Fire();
            isCharged = false;
            guidingLineRenderer.startColor = IdleColor;
            guidingLineRenderer.endColor = IdleColor;
        }

        UpdateGuidingLine();
    }

    private void UpdateGuidingLine()
    {
        Vector2 dir = playerController.AimDirection;
        float length = isCharged ? aimedLineLength : idleLineLength;
        Vector3 origin = playerController.transform.position + (Vector3)(dir * 0.5f);
        Vector3 end = origin + (Vector3)(dir * length);
        guidingLineRenderer.SetPosition(0, origin);
        guidingLineRenderer.SetPosition(1, end);
    }

    private void Fire()
    {
        if (!pickupController.TryExpendOne(out _)) return;

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        pc.Direction = playerController.AimDirection;
        pc.Shooter = playerController.gameObject;
    }
}
