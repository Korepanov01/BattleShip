using UnityEngine;

public class Medkit : MonoBehaviour
{
    private int health = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().IncreaseHealth(health);
            Destroy(gameObject);
        }
    }
}
