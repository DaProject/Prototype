using UnityEngine;
using System.Collections;

public class DropSword20Trigger : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
        {
            Debug.Log("Got the Ability Slash!!");

            other.GetComponent<PlayerManager>().sword20Active = true;

            Destroy(this.gameObject);
		}
		
	}
}
