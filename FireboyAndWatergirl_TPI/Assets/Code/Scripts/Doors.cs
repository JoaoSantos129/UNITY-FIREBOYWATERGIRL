using UnityEngine;

public class Doors : MonoBehaviour
{
    private bool isOccupied = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOccupied) // Prevent double-counting
        {
            isOccupied = true;
            SceneController.instance.DoorEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isOccupied) // Prevent negative counts
        {
            isOccupied = false;
            SceneController.instance.DoorExited();
        }
    }
}
