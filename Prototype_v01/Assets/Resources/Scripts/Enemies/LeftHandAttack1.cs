using UnityEngine;
using System.Collections;

public class LeftHandAttack1 : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
        if (other.tag == "Player")
        {
            Debug.Log("Player Attacked");
            other.GetComponent<PlayerManager>().setDamaged(transform.root.GetComponent<EnemyPumpkinManager>().attackDamage);
        }
	}
}
