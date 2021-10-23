using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform targetTranform;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (targetTranform != null)
        {
            RotateToTarget();
        }
        else
        {
            FindTarget();
        }
    }

    void FindTarget()
    {
        var target = GameObject.FindWithTag("EnemyIsland");
        if (target == null)
        {
            target = GameObject.FindWithTag("Boss");
        }
        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
        }
        targetTranform = target.transform;
    }

    void RotateToTarget()
    {
        var vectorToTarget = targetTranform.position - playerTransform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        var quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime);
    }
}
