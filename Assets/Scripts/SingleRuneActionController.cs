using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SingleRuneActionController : MonoBehaviour
{
    [SerializeField] private PickupController pickupController;
    private PlayerController playerController;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private bool r1WasPressed;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        var gamepad = playerController.assignedGamepad;
        if (gamepad == null) return;

        bool r1Pressed = gamepad.rightShoulder.isPressed;
        if (r1Pressed && !r1WasPressed)
        {
            if (pickupController.TryExpendOne(out _))
            {
                Transform spawnPoint = firePoint != null ? firePoint : transform;
                GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.Direction = playerController.FacingDirection;
                pc.Shooter = playerController.gameObject;
            }
        }

        r1WasPressed = r1Pressed;
    }
}
