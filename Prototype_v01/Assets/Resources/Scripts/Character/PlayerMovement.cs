using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rigidBody;
	Animator anim;
    public float turnSmoothing;                     // A smoothing value for turning the player.
    public float speedDampTime;                     // The damping for the speed parameter.
    private float moveHorizontal, moveVertical;

    void Start()
    {
		anim = GetComponent <Animator> ();
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");   // Gets the horizontal axis
        moveVertical = Input.GetAxis("Vertical");       // Gets the vertical axis

        MovementManagement(moveHorizontal, moveVertical);
		Animating (moveHorizontal, moveVertical);

    }

    void MovementManagement(float horizontal, float vertical)
    {
        //If there is some axis input...
        if(horizontal != 0 || vertical != 0)
        {
            // ...set the players rotation and set the speed paramter to 5.5f
            Rotating(horizontal, vertical);
        }
    }

    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);

        // Create a rotation based on this new vector assuming that up is the global axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a reotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rigidBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        rigidBody.MoveRotation(newRotation);
    }

	void Animating(float horizontal, float vertical)
	{
        // TODO: not working properly: animation not reactivating after deactivating the playerController
		bool walking = horizontal != 0f || vertical != 0f;
		anim.SetBool ("IsWalking", walking);
	}

}
