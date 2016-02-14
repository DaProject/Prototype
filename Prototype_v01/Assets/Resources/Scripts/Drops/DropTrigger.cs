using UnityEngine;
using System.Collections;

public class DropTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") 
		{	
			other.GetComponent<PlayerManager> ().slashActive = true;

			Destroy(this.gameObject);
		}
	}
}
