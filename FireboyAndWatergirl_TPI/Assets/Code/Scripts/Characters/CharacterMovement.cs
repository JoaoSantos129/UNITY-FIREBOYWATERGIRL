using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D body;
    [SerializeField] public float speed;

    protected Animator bodyAnimator;
    protected Animator headAnimator;
    protected bool grounded;
    protected Transform headTransform;
    protected Vector3 originalHeadPosition;
    protected bool jumpReleased = true;

    // Each child class will define its own input axis names
    protected abstract string HorizontalInput { get; }
    protected abstract KeyCode JumpKey { get; }

    // Abstract property that child classes override to define their specific head offsets
    protected abstract Vector3 RunningHeadOffset { get; }
    protected abstract Vector3 JumpingHeadOffset { get; }

    public virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        bodyAnimator = transform.Find($"{gameObject.name}_Body").GetComponent<Animator>();
        headAnimator = transform.Find($"{gameObject.name}_Head").GetComponent<Animator>();

        headTransform = transform.Find($"{gameObject.name}_Head");
        originalHeadPosition = headTransform.localPosition;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis(HorizontalInput);

        // Apply movement
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Jump logic
        if (Input.GetKeyDown(JumpKey) && grounded && jumpReleased)
        {
            Jump();
            jumpReleased = false; // Prevent multiple jumps while holding jump the key
        }

        // Reset jumpReleased when the key is released
        if (Input.GetKeyUp(JumpKey))
        {
            jumpReleased = true;
        }

        // Flip sprite direction
        if (horizontalInput > 0.01f) // Moving right
        {
            transform.localScale = new Vector3(1, 1.6f, 1);

            if (grounded)
                AdjustHeadPosition(RunningHeadOffset);
            else
                AdjustHeadPosition(JumpingHeadOffset); // Apply jump-specific offset
        }
        else if (horizontalInput < -0.01f) // Moving left
        {
            transform.localScale = new Vector3(-1, 1.6f, 1);

            if (grounded)
                AdjustHeadPosition(RunningHeadOffset);
            else
                AdjustHeadPosition(JumpingHeadOffset); // Apply jump-specific offset
        }
        else
        {
            // If the character is not moving horizontally, check if in the air and adjust the head position
            if (!grounded)
            {
                AdjustHeadPosition(JumpingHeadOffset); // Apply jumping offset even when idle
            }
            else
            {
                ResetHeadPosition(); // Reset when idle and grounded
            }
        }

        // Set animator parameters
        bool isRunning = horizontalInput != 0;
        bodyAnimator.SetBool("run", isRunning);
        bodyAnimator.SetBool("grounded", grounded);
        headAnimator.SetBool("run", isRunning);
        headAnimator.SetBool("grounded", grounded);
    }



    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed * 1.6f);
        grounded = false;

        // Apply unique jumping offset when airborne
        AdjustHeadPosition(JumpingHeadOffset);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;

            // Reset head position to match running offset or idle position
            ResetHeadPosition();
        }
    }


    private void AdjustHeadPosition(Vector3 offset)
    {
        headTransform.localPosition = originalHeadPosition + offset;
    }

    private void ResetHeadPosition()
    {
        headTransform.localPosition = originalHeadPosition;
    }
}
