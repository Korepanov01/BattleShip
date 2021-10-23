using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource splashSound;
    public AudioSource hitSound;
    public float firingRange = 0.4f;
    public int damage;
    public string fromTag;

    private Coroutine sinkCoroutine;

    private void Start()
    {
        sinkCoroutine = StartCoroutine(Sink(firingRange));
    }

    IEnumerator Sink(float time)
    {
        var animator = GetComponent<Animator>();

        yield return new WaitForSeconds(time);

        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        transform.position += new Vector3(0, 0, 1);
        splashSound.pitch = Random.Range(0.9f, 1.1f);
        splashSound.Play();
        animator.Play("Sink");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        StopCoroutine(sinkCoroutine);

        var otherHealth = other.GetComponent<Health>();
        var animator = GetComponent<Animator>();

        if (otherHealth != null && !other.gameObject.CompareTag(fromTag) &&
            !(fromTag == "Boss" && other.gameObject.CompareTag("Enemy") || fromTag == "Enemy" && other.gameObject.CompareTag("Boss")) &&
            !otherHealth.dead)
        {
            otherHealth.TakeDamage(damage);
        }

        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        transform.position += new Vector3(0, 0, -1);
        hitSound.pitch = Random.Range(0.9f, 1.1f);
        hitSound.Play();
        animator.Play("Hit");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
