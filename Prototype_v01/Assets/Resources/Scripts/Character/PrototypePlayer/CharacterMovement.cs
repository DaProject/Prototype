using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    
    public float turnSmoothing;                     // A smoothing value for turning the player.
    public float speedDampTime;                     // The damping for the speed parameter.
    private float moveHorizontal, moveVertical;   

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");   // Gets the horizontal axis
        moveVertical = Input.GetAxis("Vertical");       // Gets the vertical axis
    }

}
