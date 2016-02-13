using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    /*
	// States of the player
	public enum EnemyStates {AWAKE, IDLE, MOVING, ATTACK, DAMAGED, DEAD}
	[Header("States")]
	public EnemyStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

    // Controller
    [Header("Controller")]
    NavMeshAgent nav;                               // Gets the navmesh of the enemy

    // Damage
    [Header("Attack")]
    public int attackDamage;
    public int meleeAttackDamage;
	public bool playerInRange;
	GameObject player;

    //Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
    AudioSource enemyAudio;

	// Timers
	public float temp;
	public float tempDamage;
    public float tempMeleeAttackDamage;
    public float attackStateCounter;

    // Control enemy
    [Header("Control")]
<<<<<<< HEAD
=======
	Transform playerPosition;
	NavMeshAgent nav;
	//private EnemyMovement EnemyMovement;
>>>>>>> origin/master
    private Rigidbody rigidBody;

    // Scripts calls
	PlayerManager playerManager;

    // Animations
    Animator anim;

	// Use this for initialization
	void Start ()
    {
		setAwake (); 				// Call the setAwake function
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch (state) {
			case EnemyStates.AWAKE:
				AwakeBehaviour();
				break;
			case EnemyStates.IDLE:
				IdleBehaviour();
				break;
			case EnemyStates.MOVING:
				MovingBehaviour();
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

	// Behaviours
	private void AwakeBehaviour()
	{
		setMoving ();     // Initalization
	}

	private void IdleBehaviour()
    {
		anim.SetTrigger ("PlayerDead");
	}

	private void MovingBehaviour()
	{
        MovingAction();

        if (playerInRange) setAttack();
	}

    private void AttackBehaviour()
    {
		if (playerManager.currentHealth <= 0) setIdle ();               // Checks if the player is already dead, if so, goes to the setIdle function

		attackStateCounter -= Time.deltaTime;                           // Starts the countdown after the attack has been done

        if (attackStateCounter <= 0 && playerInRange)
        {
            setAttack();
        }
        else if (!playerInRange) setMoving();
    }
    private void DamagedBehaviour()
	{
		
	}
	private void DeadBehaviour()
	{

	}
    
	// Sets
	public void setAwake()
    {
        Debug.Log("setAwakeEnemy");

        currentHealth = maxHealth;                              // Sets the enemy health to the value of maxHealth that you indicated

        player = GameObject.FindGameObjectWithTag("Player");    // Finds the gameobject with the tag "Player". Finds the player               // Gets the transform of the player

        playerManager = player.GetComponent<PlayerManager>();   // Gets the script PlayerManager of the player

        nav = GetComponent<NavMeshAgent>();                     // Gets the NavMeshAgent component

        enemyAudio = GetComponent<AudioSource>();               // Gets the AudioSource component

        anim = GetComponent<Animator>();                        // Gets the Animator component

        playerInRange = false;

        state = EnemyStates.AWAKE;                              // Calls the AWAKE state
	}

	public void setIdle()
    {
        Debug.Log("setIdleEnemy");

        anim.SetTrigger("PlayerDead");

        state = EnemyStates.IDLE;      // Calls the IDLE state
	}
	public void setMoving()
    {
        Debug.Log("setMoveEnemy");

        state = EnemyStates.MOVING;
	}
    public void setAttack()
    {
        Debug.Log("setAttackEnemy");

        AttackAction(meleeAttackDamage, tempMeleeAttackDamage);

		anim.SetTrigger("isAttacking");        // Plays the attack animation

		state = EnemyStates.ATTACK;     // Goes to the attack state
    }

    public void setDamaged(int damage)
	{
		temp = tempDamage;

		currentHealth -= damage;                // Applies the damage recieved

		if (currentHealth <= 0) setDead ();     // Calls the setDead function if the enemy has died

		else state = EnemyStates.DAMAGED;      // If the enemy is still alive, calls the DAMAGED state
	}

	public void setDead()
	{
		currentHealth = 0;                      // Sets the health to 0

        anim.SetTrigger("Dead");

		state = EnemyStates.DEAD;              // Calls the DEAD state
	}

	void OnTriggerEnter (Collider other)
	{
        if (other.tag == "Player") playerInRange = true;
	}

	void OnTriggerExit (Collider other)
	{
        if (other.tag == "Player") playerInRange = false;
    }

    void AttackAction(int damageDealt, float attackDuration)
    {

        attackDamage = damageDealt;                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;        // Sets the amount of time that the player has to be in the attackXX state.
    }

    void MovingAction()
    {
        nav.SetDestination(player.transform.position);

        anim.SetTrigger("PlayerFound");
    }*/	
}
