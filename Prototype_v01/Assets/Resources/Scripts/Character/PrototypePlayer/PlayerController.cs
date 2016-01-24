using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float moveHorizontal, moveVertical;
    private Vector3 movement;
    public float speed;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();      // Gets the Rigidbody from the GameObject.
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");   // Gets the input horizontal axis.
        moveVertical = Input.GetAxis("Vertical");       // Gets the input vertical axis.

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBody.velocity = movement * speed;
    }
}
