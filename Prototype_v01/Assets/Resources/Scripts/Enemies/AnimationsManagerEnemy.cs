using UnityEngine;
using System.Collections;

public class AnimationsManagerEnemy : MonoBehaviour {

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
	
		if (Input.GetKeyUp (KeyCode.Keypad5)) {

			myAnim.Stop();

			myAnim.clip = idle;

			myAnim.Play();
		}

		if (Input.GetKeyUp (KeyCode.Keypad6)) {

			myAnim.Stop();

			myAnim.clip = run;

			myAnim.Play();
		}

		if (Input.GetKeyUp (KeyCode.Keypad7)) {

			myAnim.Stop();

			myAnim.clip = attack;

			myAnim.Play();
		}

		if (Input.GetKeyUp (KeyCode.Keypad8)) {

			myAnim.Stop();

			myAnim.clip = death;

			myAnim.Play();
		}
	}
}
