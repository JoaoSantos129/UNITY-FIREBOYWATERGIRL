using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene transitions and tracks door occupancy for level progression.
/// Designed for a 2D co-op game like Fireboy and Watergirl.
/// </summary>
public class SceneController : MonoBehaviour
{
    // Singleton instance so other scripts can access SceneController easily
    public static SceneController Instance { get; private set; }

    // Keeps track of how many players are currently at the door
    private int doorsOccupied = 0;

    private void Awake()
    {
        // Set screen resolution to 1920x1080 and enable fullscreen
        // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Screen.SetResolution.html
        bool fullscreen = true; 
        Screen.SetResolution(1920, 1080, fullscreen);

        // Implement Singleton pattern to keep one instance across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate if one already exists
        }
    }

    /// <summary>
    /// Called when a player enters the door.
    /// Increments the occupied door count and checks if both players are ready to proceed.
    /// </summary>
    public void DoorEntered()
    {
        doorsOccupied++;

        // If both players are at their respective doors, move to the next level
        if (doorsOccupied >= 2)
        {
            NextLevel();
        }
    }

    /// <summary>
    /// Called when a player exits the door area.
    /// Decrements the occupied door count.
    /// </summary>
    public void DoorExited()
    {
        doorsOccupied--;
    }

    /// <summary>
    /// Restarts the current level.
    /// Gets called when at least one player dies.
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads the next level in the build index.
    /// Called automatically when both players reach the doors simultaneously.
    /// </summary>
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Loads a specific scene by name.
    /// Used for menus, cutscenes, or specific levels.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}