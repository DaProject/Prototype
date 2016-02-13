using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyPumpkinManager : MonoBehaviour
{
    
    // States of the enemy
    public enum EnemyStates {AWAKE, IDLE, ACTIVE, ATTACK, DAMAGED, DEAD}
    [Header("States")]
    public EnemyStates state;

    // Health
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    // NavMesh
    [Header("NavMesh")]
    NavMeshAgent nav;                           // NavMesComponent 
    GameObject player;                          // Player
    
    // Damage
    [Header("Attack")]
    public int attackDamage;                    // Auxiliar variable that gets the value of the differents attacks. After it is used to apply the damage to the player.
    public int attackMelee;                     // Variable with the damage of the enemy attack.
    public bool playerInRange;                  // Bool that is true when the player is in attack range.

    // Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
    AudioSource enemyAudio;

    // Timers
    [Header("Timers")]
    public float temp;
    public float tempDamage;                    // Counter that determinates how much time the enemy has to be in the DAMAGED state.               
    public float tempAttackMelee;               // Counter that reflects how much the enemy attack longs.
    public float attackStateCounter;            // Auxiliar variable that says how much time the enemy has to be in the ATTACK state. It gets the value from the counter of the attack.

    // Control enemy
    [Header("Control")]
    private Rigidbody rigidBody;                // Rigidbody component from the enemy.

    // Animations
    Animator anim;                              // Animator from the enemy.

    // Scripts calls
    PlayerManager playerManager;                // PlayerManager script

	// Use this for initialization
	void Start ()
    {
        setAwake();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (state)
        {
            case EnemyStates.AWAKE:
                AwakeBehaviour();
                break;
            case EnemyStates.IDLE:
                IdleBehaviour();
                break;
            case EnemyStates.ACTIVE:
                ActiveBehaviour();
                break;
            case EnemyStates.ATTACK:
                AttackBehaviour();
                break;
            case EnemyStates.DAMAGED:
                DamagedBehaviour();
                break;
            case EnemyStates.DEAD:
                DeadBehaviour();
                break;
        }
	}

    // Behaviorus
    private void AwakeBehaviour()
    {
        setActive();
    }

    private void IdleBehaviour()
    {
        anim.SetTrigger("Idle");                            // It triggers the enemy Idle animation.
    }

    private void ActiveBehaviour()
    {
        ControllerAction();                                 // Calls the ControllerAction function when the enemy has to move.

        if (playerInRange) setAttack();                     // Calls the setAttack function if the player is in range attack.
    }

    private void AttackBehaviour()
    {
        attackStateCounter -= Time.deltaTime;               // Starts the countdown after the attack has been done.

        if (attackStateCounter <= 0) setActive();           // Goes back to setIdle if the enemy has not attack for a small amount of time.
    }

    private void DamagedBehaviour()
    {
        temp -= Time.deltaTime;

        if (temp <= 0) setIdle();
    }

    private void DeadBehaviour()
    {

    }

    // Sets
    public void setAwake()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");    // Finds the gameobject with the tag "Player".

        playerManager = player.GetComponent<PlayerManager>();   // Gets the script PlayerManager of the player.

        nav = GetComponent<NavMeshAgent>();                     // Gets the NavMeshAgent component.

        playerInRange = false;                                  // Initalize the playerInRange bool to false.

        enemyAudio = GetComponent<AudioSource>();               // Gets the AudioSource component from the enemy.

        rigidBody = GetComponent<Rigidbody>();                  // Gets the rigidbody component from the enemy.

        anim = GetComponent<Animator>();                        // Gets the animator component from the enemy.

        state = EnemyStates.AWAKE;                              // Goes to the AWAKE state.
    }

    public void setIdle()
    {

    }

    public void setActive()
    {
        anim.SetBool("Attack", false);                          // Sets the Attack bool for the attack animation to false.

        state = EnemyStates.ACTIVE;                             // Goes to the ACTIVE state.
    }

    public void setAttack()
    {
        AttackAction(attackMelee, tempAttackMelee);             // Calls the AttackAction function, and give it the damage  fo the attack, and the time that longs.

        anim.SetBool("Attack", true);                           // Sets the Attack bool for the attack animation to true.

        anim.SetBool("Run", false);                             // Sets the Run bool for the run animation to true.

        state = EnemyStates.ATTACK;                             // Goes to the ATTACK state.
    }

    public void setDamaged(int damage)
    {
        temp = tempDamage;

        currentHealth -= damage;                                // Applies the damage recieved

        if (currentHealth <= 0) setDead();                      // Calls the setDead function if the enemy has died

        else state = EnemyStates.DAMAGED;                       // If the enemy is still alive, calls the DAMAGED state
    }

    public void setDead()
    {

    }

    private void ControllerAction()
    {
        nav.SetDestination(player.transform.position);                      // Sets the destination of the navmesh to the actual player position. The enemy is goin to go there.

        anim.SetBool("Run", true);                                          // Sets the Run bool for the run animation to true.
    }

    private void AttackAction(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                         // Sets the amount of damage that the enemy does with this attack.

        attackStateCounter = attackDuration;                                // Sets the amount of time that the enemy has to be in the attack state.
    }

    void  OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") playerInRange = true;                    // Sets the playerInRange to true if the player has been detected around the enemy.
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") playerInRange = false;                   // Sets the playerInRange to true if the player has exit the area detection of the enemy.
    }
}
