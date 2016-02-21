using UnityEngine;
using System.Collections;

public class DropChain10Trigger : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
        {
            Debug.Log("Got the Ability Slash!!");

            other.GetComponent<PlayerManager>().chain10Active = true;

            Destroy(this.gameObject);
		}
		
	}
}
