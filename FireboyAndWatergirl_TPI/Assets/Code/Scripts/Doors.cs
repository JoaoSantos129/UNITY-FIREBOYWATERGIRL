using UnityEngine;

/// <summary>
/// Handles door logic, detecting when a player enters or exits the door trigger area.
/// Informs the SceneController about door occupancy to trigger level progression.
/// </summary>
public class Doors : MonoBehaviour
{
    // Boolean flag to track whether the door is occupied by a player
    private bool isOccupied = false;

    /// <summary>
    /// Called when another collider enters the door's trigger area.
    /// Informs SceneController that the door has been entered.
    /// Prevents double-counting when the same player re-enters.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger if the door is not already occupied (to prevent multiple triggers)
        if (!isOccupied)
        {
            isOccupied = true; // Mark the door as occupied
            SceneController.Instance.DoorEntered(); // Inform the SceneController that the door has been entered
        }
    }

    /// <summary>
    /// Called when another collider exits the door's trigger area.
    /// Informs SceneController that the door is no longer occupied.
    /// Prevents negative counts when the player leaves the door area.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        // Only trigger if the door is occupied (to prevent unnecessary state changes)
        if (isOccupied)
        {
            isOccupied = false; // Mark the door as unoccupied
            SceneController.Instance.DoorExited(); // Inform the SceneController that the door has been exited
        }
    }
}
