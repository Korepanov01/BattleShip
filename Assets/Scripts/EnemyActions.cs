using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public float attackRadius = 2f;
    public float speed;
    public float rotationSpeed;

    private Transform playerTransform;
    private CannonShooting cannonShooting;
    private bool firstAttack = true;

    private void Start()
    {
        cannonShooting = GetComponent<CannonShooting>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        var gameController = GameObject.FindWithTag("EnemiesController").GetComponent<EnemiesController>();
        speed = gameController.enemySpeed;
        rotationSpeed = gameController.enemyRotationSpeed;
        cannonShooting.damage = gameController.enemyDamage;
        cannonShooting.overhit = gameController.enemyOverhit;
    }

    void Update()
    {
        if (IsPlayerInAttackRadius())
        {
            AttackPlayer();
            firstAttack = false;
        }
        else
        {
            firstAttack = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, AngleToPlayer(-90), Time.deltaTime * rotationSpeed);
            transform.Translate(new Vector3(0, 1) * speed * Time.deltaTime);
        }
    }

    bool IsPlayerInAttackRadius()
    {
        return Mathf.Pow(playerTransform.position.x - transform.position.x, 2) + Mathf.Pow(playerTransform.position.y - transform.position.y, 2) < attackRadius * attackRadius;
    }

    void AttackPlayer()
    {
        float rotateAngle;

        if (firstAttack)
        {
            cannonShooting.timer = 40;
        }
        if (IsPlayerOnRight())
        {
            cannonShooting.Shoot(CannonShooting.Direction.right);
            rotateAngle = 0;
        }
        else
        {
            cannonShooting.Shoot(CannonShooting.Direction.left);
            rotateAngle = 180;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, AngleToPlayer(rotateAngle), Time.deltaTime * rotationSpeed);
        transform.Translate(new Vector3(0, 1) * speed * Time.deltaTime);
    }

    Quaternion AngleToPlayer(float rotateAngle)
    {
        var vectorToTarget = playerTransform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + rotateAngle;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    bool IsPlayerOnRight()
    {
        var vectorToTarget = playerTransform.position - transform.position;
        float dotProduct = Vector3.Dot(vectorToTarget, transform.right);
        return dotProduct > 0;
    }
}
