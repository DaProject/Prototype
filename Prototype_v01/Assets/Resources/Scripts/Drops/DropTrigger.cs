using UnityEngine;
using System.Collections;

public class DropTrigger : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
        {
            Debug.Log("Got the Ability Slash!!");

            other.GetComponent<PlayerManager> ().slashActive = true;

			Destroy(this.gameObject);
		}
		
	}
}
