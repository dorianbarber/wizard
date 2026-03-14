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
    // Unity looks for functions with the prefix "On" to handle events. Move is an action specific attached to the player object. 
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
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
            // animator.SetFloat("MoveX", 0);
            // animator.SetFloat("MoveY", 0);
        }
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        if (gamepad.rightTrigger.isPressed)
        {
            var rightStick = gamepad.rightStick.ReadValue();
            if (rightStick.sqrMagnitude > 0.01f)
                FacingDirection = rightStick.normalized;
        }
    }

    void FixedUpdate()
    {
        // 8-directional movement logic
        if (movementEnabled)
            rb.linearVelocity = moveInput * moveSpeed;
    }
}
