using UnityEngine;
using System.Collections;

public class DropGameObject : MonoBehaviour {

	public GameObject drop;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			//Destroy(gameObject);
			Instantiate(drop, transform.position, Quaternion.identity);
		}
	}
}

