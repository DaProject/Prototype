using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Controller")]
    private PlayerManager playerManager;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;

    public int attackDamage;

    public int damageAttack10;                      // Variable with the damage of the attack10 (sword).
    public int damageSword10;                       // Variable with the damage of the sword10 (sword hability).
    public int damageAttack01;                      // Variable with the damage of the attack01 (chain).
    public int damageChain01;                       // Variable with the damage of the chain01 (chain hability).
    public float tempSword10Stun;                   // Counter that determinates when the sword10 attack is going to stun the enemies.

	// Use this for initialization
	void Start ()
    {
        playerManager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
	}

    public void Attack10(int damageDealt)
    {
        Debug.Log("Attack10");

        playerManager.attackStateCounter = playerAnimation.tempAttack10;

        playerManager.attack10Collider.enabled = true;

        playerController.playerDisplacementSpeed = playerController.AnimationDisplacement(playerController.trans.localPosition.z + playerController.distanceAttack10, playerAnimation.tempAttack10);

        playerAnimation.Attack10Animation();

        attackDamage = damageDealt;
    }

    public void Attack01(int damageDealt)
    {
        Debug.Log("Attack01");

        playerManager.attack01Collider.enabled = true;

        attackDamage = damageDealt;
    }

    public void Sword10(int damageDealt)
    {
        Debug.Log("Sword10");

        playerManager.sword10Collider.enabled = true;

        attackDamage = damageDealt;
    }

    public void Chain01(int damageDealt)
    {
        Debug.Log("Chain01");

        playerManager.chain01Collider.enabled = true;

        attackDamage = damageDealt;
    }

    public void Dash()
    {
        playerManager.playerSphereCollider.enabled = false;
        playerManager.playerCapsuleCollider.enabled = false;
        playerController.rigidBody.useGravity = false;
    }
}
