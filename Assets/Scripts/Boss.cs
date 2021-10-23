using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public AudioSource deathSound;

    private int maxHealth = 1200;

    private void Start()
    {
        GetComponent<Health>().SetMaxHealth(maxHealth);
        GetComponent<CannonShooting>().bulletSpeed = 5;
    }

    public IEnumerator Death()
    {
        var animator = GetComponent<Animator>();
        Destroy(GetComponent<EnemyActions>());
        animator.Play("Death");
        deathSound.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);
        Application.Quit();

        Destroy(gameObject);
    }
}
