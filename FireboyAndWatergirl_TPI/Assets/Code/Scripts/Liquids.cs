using UnityEngine;

public class Liquids : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneController.instance.RestartLevel();
    }
}