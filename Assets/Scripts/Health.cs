using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int health;
    public HealthBar healthBar;
    public bool dead = false;

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            dead = true;
            if (CompareTag("Player"))
            {
                StartCoroutine(GetComponent<Player>().Death());
            }
            else if (CompareTag("Enemy"))
            {
                GetComponent<Enemy>().Death();
            }
            else if (CompareTag("Boss"))
            {
                StartCoroutine(GetComponent<Boss>().Death());
            }
        }
    }

    public void IncreaseMaxHealth(int health)
    {
        maxHealth += health;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        this.health = health;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    public void IncreaseHealth(int health)
    {
        this.health += health;
        healthBar.SetHealth(this.health);
    }

    public void RefillHealth()
    {
        health = maxHealth;
        healthBar.SetHealth(maxHealth);
        dead = false;
    }
}
