using UnityEngine;
using TMPro; // TextMesh Pro namespace for UI text handling

/// <summary>
/// Handles the timer logic to display elapsed time in the format MM:SS.
/// Updates every frame to track how long the level has been running.
/// </summary>
public class Timer : MonoBehaviour
{
    // Reference to the TextMeshPro UI element where the timer will be displayed
    [SerializeField] TextMeshProUGUI timerText;

    // The elapsed time in seconds
    float elapsedTime;

    /// <summary>
    /// Called once per frame to update the timer.
    /// </summary>
    void Update()
    {
        // Increment elapsed time by the time passed since the last frame
        elapsedTime += Time.deltaTime;

        // Calculate minutes and seconds from the elapsed time
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Get whole minutes
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Get remaining seconds

        // Update the timerText with the formatted time (MM:SS)
        // {00:00} ensures two digits for minutes and {01:00} ensures two digits for seconds
        timerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
}
