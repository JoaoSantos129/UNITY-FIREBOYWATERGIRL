using UnityEngine;

/// <summary>
/// Class that handles character movement, including jumping, running, and animations.
/// This is designed to be reused by multiple character types by assigning a different CharacterConfig.
/// </summary>
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body; // Physics body used to move the character
    [SerializeField] private float speed; // Base movement speed of the character

    [SerializeField] protected CharacterConfig characterConfig; 
    // The ScriptableObject containing character-specific configuration:
    // - horizontal input axis
    // - jump key
    // - head animation offsets

    // Animator components for controlling body and head animations
    protected Animator bodyAnimator;
    protected Animator headAnimator;

    // References for tracking and adjusting the character’s head
    protected Transform headTransform; // Transform of the head object
    protected Vector3 originalHeadPosition; // Used to reset the head to its default position

    protected bool grounded;         // Is the character currently on the ground?
    protected bool jumpReleased = true; // Ensures jump triggers once per key press

    private string HorizontalInput => characterConfig.horizontalInputAxis; // e.g., "HorizontalP1"
    private KeyCode JumpKey => characterConfig.jumpKey; // e.g., KeyCode.Space
    private Vector3 RunningHeadOffset => characterConfig.runningHeadOffset; // Head bobbing while running
    private Vector3 JumpingHeadOffset => characterConfig.jumpingHeadOffset; // Head movement during jump

    /// <summary>
    /// Called when the object is first created or enabled. Initializes necessary references.
    /// </summary>
    public virtual void Awake()
    {
        // Get Rigidbody if not manually assigned
        body = GetComponent<Rigidbody2D>();

        // Get body/head animators and transforms based on naming convention
        bodyAnimator = transform.Find($"{gameObject.name}_Body").GetComponent<Animator>();
        headAnimator = transform.Find($"{gameObject.name}_Head").GetComponent<Animator>();
        headTransform = transform.Find($"{gameObject.name}_Head");

        // Store the original head position for later resets
        originalHeadPosition = headTransform.localPosition;
    }

    /// <summary>
    /// Runs every frame to check input and update movement and animation.
    /// </summary>
    private void Update()
    {
        // Read horizontal movement input from axis configured in CharacterConfig
        float horizontalInput = Input.GetAxis(HorizontalInput);

        // Move character based on horizontal input (preserving vertical movement)
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Handle jump input: only jump if grounded and jump wasn't already triggered
        if (Input.GetKeyDown(JumpKey) && grounded && jumpReleased)
        {
            Jump(); // Apply jump logic
            jumpReleased = false; // Prevent multiple jumps until key is released
        }

        // Allow jump again when the jump key is released
        if (Input.GetKeyUp(JumpKey))
        {
            jumpReleased = true;
        }

        // Flip character and adjust head based on movement direction
        HandleCharacterDirection(horizontalInput);

        // Update body and head animations based on current state
        UpdateAnimations(horizontalInput);
    }

    /// <summary>
    /// Adjusts the character’s facing direction and head position depending on movement.
    /// </summary>
    private void HandleCharacterDirection(float horizontalInput)
    {
        if (horizontalInput > 0.01f)
        {
            // Moving right
            transform.localScale = new Vector3(1, 1.6f, 1); // Face right
            AdjustHeadPosition(grounded ? RunningHeadOffset : JumpingHeadOffset);
        }
        else if (horizontalInput < -0.01f)
        {
            // Moving left
            transform.localScale = new Vector3(-1, 1.6f, 1); // Face left
            AdjustHeadPosition(grounded ? RunningHeadOffset : JumpingHeadOffset);
        }
        else
        {
            // Not moving horizontally
            if (!grounded)
                AdjustHeadPosition(JumpingHeadOffset); // Airborne
            else
                ResetHeadPosition(); // Standing still
        }
    }

    /// <summary>
    /// Updates running and grounded animation states.
    /// </summary>
    private void UpdateAnimations(float horizontalInput)
    {
        bool isRunning = horizontalInput != 0;

        // Set animation parameters for both body and head
        bodyAnimator.SetBool("run", isRunning);
        bodyAnimator.SetBool("grounded", grounded);
        headAnimator.SetBool("run", isRunning);
        headAnimator.SetBool("grounded", grounded);
    }

    /// <summary>
    /// Handles jumping mechanics and head movement during the jump.
    /// </summary>
    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed * 1.6f); // Apply upward velocity
        grounded = false; // No longer on the ground
        AdjustHeadPosition(JumpingHeadOffset); // Adjust head for jump animation
    }

    /// <summary>
    /// Checks collision with ground to reset grounded state and head position.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true; // Landed on ground
            ResetHeadPosition(); // Head returns to original position
        }
    }

    /// <summary>
    /// Moves the character's head based on the provided animation offset.
    /// </summary>
    private void AdjustHeadPosition(Vector3 offset)
    {
        headTransform.localPosition = originalHeadPosition + offset;
    }

    /// <summary>
    /// Restores the head to its original resting position.
    /// </summary>
    private void ResetHeadPosition()
    {
        headTransform.localPosition = originalHeadPosition;
    }
}
