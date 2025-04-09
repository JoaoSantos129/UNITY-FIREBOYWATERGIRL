using UnityEngine;

/// <summary>
/// Handles the collection of diamonds by the player.
/// When the player touches a diamond, it is destroyed and the count is incremented.
/// </summary>
public class Diamonds : MonoBehaviour
{
    // Counter to track the number of diamonds collected
    private int diamondCounter;

    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// Increments the diamond counter and destroys the diamond game object.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Increment the diamond counter when a collision with a player is detected
        diamondCounter++;

        // Destroy the current diamond game object
        Destroy(gameObject);
    }
}
