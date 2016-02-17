using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Exit");
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log ("Exit");
		Application.Quit();
	}
}
