using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{

    // WARNING: NO LONGER USED

    private Rigidbody rigidBody;
	Animator anim;



	// Use this for initialization
	void Start () {
	
		anim = GetComponent <Animator> ();
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			anim.SetTrigger ("IsDashing");
		}
	}
}
