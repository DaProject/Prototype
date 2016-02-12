using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	// States of the player
	public enum EnemyStates {AWAKE, IDLE, MOVING, ATTACK, DAMAGED, DEAD}
	[Header("States")]
	public EnemyStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

    // Damage
    [Header("Attack")]
    public int damageDealt;
    public float attackStateCounter;
	bool playerInRange;
	GameObject player;
	public float timeBetweenAttacks = 2.2f;
	public int attackDamage = 10;

    //Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
    AudioSource playerAudio;

	// Timers
	public float temp;
	public float tempDamage;
	public float timerMoving;
	public float timerAttacking;

    // Control enemy
    [Header("Control")]
	Transform playerPosition;
	NavMeshAgent nav;
	private EnemyMovement EnemyMovement;
    private Rigidbody rigidBody;
	PlayerManager playerManager;

    // Animations
    Animator anim;

	// Use this for initialization
	void Start ()
    {
		setAwake (); 				// Call the setAwake function

		player = GameObject.FindGameObjectWithTag ("Player");

		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;

		playerManager = player.GetComponent <PlayerManager> ();

		nav = GetComponent <NavMeshAgent> ();
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
		if (playerManager.currentHealth <= 0) 
		{
			anim.SetTrigger ("PlayerDead");
		}
	}

	private void MovingBehaviour()
	{
		nav.SetDestination (playerPosition.position);

		timerAttacking += Time.deltaTime;

		if (timerAttacking >= timeBetweenAttacks && playerInRange /* && enemyHealth.currentHealth > 0*/)
		{
			setAttack ();
		}

		if (playerManager.currentHealth <= 0) 
		{
			anim.SetTrigger ("PlayerDead");
		}
	}

    private void AttackBehaviour()
    {
		if (playerManager.currentHealth <= 0) 
		{
			anim.SetTrigger ("PlayerDead");

			setIdle ();
		}

		timerMoving += Time.deltaTime;

		if (timerMoving <= 2.2f) 
		{
			timerMoving = 0;
			Debug.Log ("timer moving reset");
		}

		if ((playerInRange == false) && (timerMoving >= timeBetweenAttacks))
		{
			setMoving ();
		}
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
		currentHealth = maxHealth;                              // Sets the enemy health to the value of maxHealth that you indicated

        anim = GetComponent<Animator>();

		state = EnemyStates.AWAKE;                             // Calls the AWAKE state
	}

	public void setIdle()
    {
		state = EnemyStates.IDLE;      // Calls the IDLE state
	}
	public void setMoving()
	{

		anim.SetTrigger("PlayerFound");

		state = EnemyStates.MOVING;
	}
    public void setAttack()
    {
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
		if (other.gameObject == player) 
		{
			playerInRange = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange = false;
		}
	}
		
}
