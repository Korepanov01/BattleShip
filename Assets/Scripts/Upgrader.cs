using System.Collections;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Upgrade();
            Destroy(gameObject);
        }
    }
}
