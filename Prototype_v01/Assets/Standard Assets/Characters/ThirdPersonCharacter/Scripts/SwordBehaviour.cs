using UnityEngine;
using System.Collections;

public class SwordBehaviour : MonoBehaviour {

	public Transform trans;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("q")) {
			trans.localScale = new Vector3 (0.1f, 0.1f, 2);
			//trans.position = new Vector3 (0.287f, 0.127f, -0.96f);

		}
	
	}
}
