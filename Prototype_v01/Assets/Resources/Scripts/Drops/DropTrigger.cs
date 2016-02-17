using UnityEngine;
using System.Collections;

public class DropTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("Got the Ability Slash!!");

		if (other.tag == "Player") 
		{	
			other.GetComponent<PlayerManager> ().slashActive = true;

			Destroy(this.gameObject);
		}
	}
}
