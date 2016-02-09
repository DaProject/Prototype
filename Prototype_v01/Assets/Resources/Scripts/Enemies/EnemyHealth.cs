using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    // WARNING: TEMPORARY SCRIPT, ONLY TO TEST PLAYER'S DAMAGE
    public int health;

    public void setDamaged(int damage)
    {
        Debug.Log("Getting hit");
        health -= damage;
    }
}
