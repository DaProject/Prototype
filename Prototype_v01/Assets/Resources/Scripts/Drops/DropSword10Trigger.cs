﻿using UnityEngine;
using System.Collections;

public class DropSword10Trigger : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
        {
            Debug.Log("Got the Ability Slash!!");

            other.GetComponent<PlayerManager>().sword10Active = true;

			Destroy(this.gameObject);
		}
		
	}
}
