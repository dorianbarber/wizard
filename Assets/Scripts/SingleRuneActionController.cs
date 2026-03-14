using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SingleRuneActionController : MonoBehaviour
{
    [SerializeField] private PickupController pickupController;
    private PlayerController playerController;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float aimLineLength = 5f;

    private LineRenderer guidingLineRenderer;
    private bool isAiming = false;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void EnsureInitialized()
    {
        if (guidingLineRenderer != null) return;

        var go = new GameObject("SingleRuneGuidingLine");
        go.transform.SetParent(transform);
        guidingLineRenderer = go.AddComponent<LineRenderer>();
        guidingLineRenderer.positionCount = 2;
        guidingLineRenderer.startWidth = 0.05f;
        guidingLineRenderer.endWidth = 0.05f;
        guidingLineRenderer.startColor = new Color(1f, 1f, 0f, 0.4f);
        guidingLineRenderer.endColor = new Color(1f, 1f, 0f, 0.4f);
        guidingLineRenderer.enabled = false;
    }

    void Update()
    {
        var gamepad = playerController.assignedGamepad;
        if (gamepad == null) return;

        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            if (pickupController.HasOne())
            {
                EnsureInitialized();
                isAiming = true;
                playerController.SetAimingMode(true, 0.5f);
            }
        }

        if (isAiming)
        {
            if (gamepad.rightStick.ReadValue().sqrMagnitude > 0.01f)
            {
                guidingLineRenderer.enabled = true;
                UpdateGuidingLine();
            }

            if (gamepad.rightShoulder.wasReleasedThisFrame)
                FireAndReset();
        }
    }

    private void UpdateGuidingLine()
    {
        Vector2 dir = playerController.AimDirection;
        Vector3 origin = playerController.transform.position + (Vector3)(dir * 0.5f);
        Vector3 end = origin + (Vector3)(dir * aimLineLength);
        guidingLineRenderer.SetPosition(0, origin);
        guidingLineRenderer.SetPosition(1, end);
    }

    private void FireAndReset()
    {
        isAiming = false;
        guidingLineRenderer.enabled = false;
        playerController.SetAimingMode(false);

        if (pickupController.TryExpendOne(out _))
        {
            Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
            GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.Direction = playerController.AimDirection;
            pc.Shooter = playerController.gameObject;
        }
    }
}
