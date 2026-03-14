using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    private bool movementEnabled = true;

    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
        if (!enabled)
            rb.linearVelocity = Vector2.zero;
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

        if (assignedGamepad.rightTrigger.isPressed)
        {
            var rightStick = assignedGamepad.rightStick.ReadValue();
            if (rightStick.sqrMagnitude > 0.01f)
                FacingDirection = rightStick.normalized;
            animator.SetBool("isWalking", false);
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
        // 8-directional movement logic
        if (movementEnabled)
            rb.linearVelocity = moveInput * moveSpeed;
    }
}
