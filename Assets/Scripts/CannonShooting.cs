using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    public AudioSource shootSound;
    public Transform[] leftFirePoints;
    public Transform[] rightFirePoints;
    public GameObject bulletPrefab;
    public float bulletSpeed = 3.5f;
    public int overhit = 60;
    public int damage = 10;
    public float timer = 0;

    public enum Direction
    {
        left,
        right
    }

    private void FixedUpdate()
    {
        if (timer < overhit)
        {
            timer += 1;
        }
    }

    public void Shoot(Direction direction)
    {
        if (timer >= overhit)
        {
            shootSound.pitch = Random.Range(0.9f, 1.1f);
            shootSound.Play();
            Transform[] firePoints;
            switch (direction)
            {
                case Direction.left:
                    {
                        firePoints = leftFirePoints;
                        break;
                    }
                default:
                    {
                        firePoints = rightFirePoints;
                        break;
                    }
            }
            foreach (var firePoint in firePoints)
            {
                var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                var bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.damage = damage;
                bulletScript.fromTag = tag;
                bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
            }
            timer = 0;
        }
    }
}
