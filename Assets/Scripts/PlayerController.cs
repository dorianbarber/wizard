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
    private bool isAiming = false;
    private float speedMultiplier = 1f;

    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
        if (!enabled)
            rb.linearVelocity = Vector2.zero;
    }

    public void SetAimingMode(bool aiming, float multiplier = 1f)
    {
        isAiming = aiming;
        speedMultiplier = aiming ? multiplier : 1f;
        if (aiming)
            AimDirection = FacingDirection;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public Gamepad assignedGamepad;

    void Update()
    {
        if (assignedGamepad == null) return;

        if (isAiming)
        {
            var rightStick = assignedGamepad.rightStick.ReadValue();
            if (rightStick.sqrMagnitude > 0.01f)
                AimDirection = rightStick.normalized;

            moveInput = assignedGamepad.leftStick.ReadValue();
            if (moveInput != Vector2.zero)
            {
                FacingDirection = moveInput.normalized;
                animator.SetFloat("MoveX", moveInput.x);
                animator.SetFloat("MoveY", moveInput.y);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else if (assignedGamepad.rightTrigger.isPressed)
        {
            var rightStick = assignedGamepad.rightStick.ReadValue();
            if (rightStick.sqrMagnitude > 0.01f)
                AimDirection = rightStick.normalized;

            moveInput = assignedGamepad.leftStick.ReadValue();
            if (moveInput != Vector2.zero)
            {
                FacingDirection = moveInput.normalized;
                animator.SetFloat("MoveX", moveInput.x);
                animator.SetFloat("MoveY", moveInput.y);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            var leftStick = assignedGamepad.leftStick.ReadValue();
            if (leftStick != moveInput)
            {
                moveInput = leftStick;
                if (moveInput != Vector2.zero)
                {
                    FacingDirection = moveInput.normalized;
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
        }
    }

    void FixedUpdate()
    {
        if (movementEnabled)
            rb.linearVelocity = moveInput * (moveSpeed * speedMultiplier);
    }
}
