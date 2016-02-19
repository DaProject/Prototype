using UnityEngine;
using System.Collections;

public class Chain01 : MonoBehaviour
{
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("EnemyAttacked");
            other.GetComponent<EnemyPumpkinManager>().setDamaged(transform.root.GetComponent<PlayerManager>().attackDamage);
        }
    } 
}
