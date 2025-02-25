using UnityEngine;

public class Diamonds : MonoBehaviour
{
    int diamondCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        diamondCounter++;
        Destroy(gameObject);
    }
}
