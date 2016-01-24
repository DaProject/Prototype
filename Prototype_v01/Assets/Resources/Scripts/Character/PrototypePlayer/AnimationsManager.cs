using UnityEngine;
using System.Collections;

public class AnimationsManager : MonoBehaviour {

    public AnimationClip attack;
	public AnimationClip idle;
	public AnimationClip run;
	public AnimationClip death;

    public Animation myAnim;

    Transform myTransform;

	// Use this for initialization
	void Start () {

        myTransform = this.transform;

        myAnim = myTransform.GetComponent<Animation>();

		//myAnim.Stop();

		//myAnim.clip = idle;

        //myAnim.Play();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyUp (KeyCode.Keypad1)) {

			myAnim.Stop();

			myAnim.clip = idle;

			myAnim.Play();
		}

		if (Input.GetKeyUp (KeyCode.Keypad2)) {

			myAnim.Stop();

			myAnim.clip = run;

			myAnim.Play();
		}

		if (Input.GetKeyUp (KeyCode.Keypad3)) {

			myAnim.Stop();

			myAnim.clip = attack;

			myAnim.Play();
		}

		if (Input.GetKeyUp (KeyCode.Keypad4)) {

			myAnim.Stop();

			myAnim.clip = death;

			myAnim.Play();
		}
	}
}
