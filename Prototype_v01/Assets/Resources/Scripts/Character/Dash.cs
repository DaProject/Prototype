using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

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
