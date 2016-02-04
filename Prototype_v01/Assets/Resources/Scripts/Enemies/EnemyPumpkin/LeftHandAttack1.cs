using UnityEngine;
using System.Collections;

public class LeftHandAttack1 : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
			Debug.Log ("Player Attacked");
		other.transform.GetComponent<PlayerManager> ().setDamaged (5);
	}
}
