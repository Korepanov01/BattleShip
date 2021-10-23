using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public AudioSource deathSound;
    public bool fullyUpgraded = false;

    private int upgradesCount = 0;
    private int maxuUpgradesCount = 7;

    private void Start()
    {
        GetComponent<Health>().SetMaxHealth(100);
    }

    public void Upgrade()
    {
        if (!fullyUpgraded)
        {
            var playerControl = GetComponent<PlayerControl>();
            var cannonShooting = GetComponent<CannonShooting>();

            playerControl.speed *= 1.1f;
            playerControl.rotationSpeed *= 1.1f;
            cannonShooting.damage += 10;
            cannonShooting.overhit -= 5;
            upgradesCount += 1;
            GetComponent<Health>().IncreaseMaxHealth(15);

            if (upgradesCount == maxuUpgradesCount)
            {
                fullyUpgraded = true;
            }
        }
    }

    public IEnumerator Death()
    {
        var animator = GetComponent<Animator>();
        var playerControl = GetComponent<PlayerControl>();

        playerControl.enabled = false;
        animator.Play("Death");
        deathSound.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        transform.position = new Vector2();
        transform.rotation = new Quaternion();
        playerControl.enabled = true;
        GetComponent<Health>().RefillHealth();
    }
}

