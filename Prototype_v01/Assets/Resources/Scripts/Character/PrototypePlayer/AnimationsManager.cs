using UnityEngine;
using System.Collections;

public class AnimationsManager : MonoBehaviour {

    public AnimationClip attack;
    public Animation myAnim;

    Transform myTransform;

	// Use this for initialization
	void Start () {

        myTransform = this.transform;

        myAnim = myTransform.GetComponent<Animation>();

        myAnim.Stop();

        myAnim.clip = attack;

        myAnim.Play();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
