using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public AudioSource deathSound;
    public GameObject upgraderPfefab;
    public GameObject medkitPrefab;

    private void Start()
    {
        GetComponent<Health>().SetMaxHealth(GameObject.FindWithTag("EnemiesController").GetComponent<EnemiesController>().enemyHealth);
    }

    public void Death()
    {
        var animator = GetComponent<Animator>();

        Destroy(GetComponent<EnemyActions>());
        deathSound.Play();
        animator.Play("Death");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        if (Random.Range(1, 4) % 3 == 0 && !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().fullyUpgraded)
        {
            Instantiate(upgraderPfefab, transform.position + new Vector3(0, 0, 1), new Quaternion());
        }
        if (Random.Range(1, 4) % 3 == 0)
        {
            Instantiate(medkitPrefab, transform.position + new Vector3(0, 0, 0.5f), new Quaternion());
        }
    }
}


