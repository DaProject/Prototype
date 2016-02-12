using UnityEngine;
using System.Collections;

public class PlayerDetectEnemies : MonoBehaviour

// WARNING: NO LONGER USED
{
    public int health;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "RightHandAttack1")
			//health -= 10;
			transform.parent.transform.GetComponent<PlayerManager> ().setDamaged (other.transform.GetComponent<EnemyAttack> ().attackDamage);
	}
}
