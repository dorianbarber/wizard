using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    public Vector2 AimDirection { get; private set; } = Vector2.down;
    private bool movementEnabled = true;
    private float speedMultiplier = 1f;

    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
        if (!enabled)
            rb.linearVelocity = Vector2.zero;
    }

    public void SetAimingMode(bool aiming, float multiplier = 1f)
    {
        speedMultiplier = aiming ? multiplier : 1f;
    }

    public void SetAimDirection(Vector2 dir) { AimDirection = dir; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public Gamepad assignedGamepad;

    void Update()
    {
        if (assignedGamepad == null) return;

        var rightStick = assignedGamepad.rightStick.ReadValue();
        if (rightStick.sqrMagnitude > 0.01f)
            AimDirection = rightStick.normalized;

        moveInput = assignedGamepad.leftStick.ReadValue();
        if (moveInput != Vector2.zero)
        {
            FacingDirection = moveInput.normalized;
            if (rightStick.sqrMagnitude <= 0.01f)
                AimDirection = FacingDirection;
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        if (movementEnabled)
            rb.linearVelocity = moveInput * (moveSpeed * speedMultiplier);
    }
}
