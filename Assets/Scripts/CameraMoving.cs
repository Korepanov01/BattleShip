using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = playerTransform.position + new Vector3(0, 0, -10);
    }
}
