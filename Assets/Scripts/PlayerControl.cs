using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 1;
    public float rotationSpeed = 0.25f;

    private CannonShooting cannonShooting;

    private void Start()
    {
        cannonShooting = GetComponent<CannonShooting>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (vertical < 0)
        {
            vertical = 0;
        }
        var direction = new Vector3(0, vertical + Math.Abs(horizontal), 0);
        var rotation = new Vector3(0, 0, -horizontal);
        transform.Rotate(rotation * rotationSpeed);
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            cannonShooting.Shoot(CannonShooting.Direction.left);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            cannonShooting.Shoot(CannonShooting.Direction.right);
        }
    }
}
