using UnityEngine;
using UnityEngine.InputSystem;

public class SingleRuneActionController : MonoBehaviour
{
    [SerializeField] private PickupController pickupController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private bool r1WasPressed;

    void Update()
    {
        var gamepad = Gamepad.current;
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
            }
        }

        r1WasPressed = r1Pressed;
    }
}
