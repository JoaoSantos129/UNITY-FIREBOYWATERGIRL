using UnityEngine;

/// <summary>
/// Handles the logic for when a player enters the liquid area.
/// Used to restart the level if a player falls into the liquid (e.g., water, lava, etc.).
/// </summary>
public class Liquids : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters the liquid's trigger area.
    /// Restarts the current level when a player touches the liquid.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Restart the current level when a player falls into the liquid
        SceneController.Instance.RestartLevel();
    }
}
