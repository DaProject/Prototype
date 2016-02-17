using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    void Start()
    {
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(playerTransform.transform.position.x, 0, playerTransform.transform.position.z);
    }
}
