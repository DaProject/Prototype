using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	// States of the player
	public enum PlayerStates {AWAKE, IDLE, ATTACK, DAMAGED, DEAD}
	[Header("States")]
	public PlayerStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

    // Damage
    [Header("Attack")]
    public int damageDealt;
    public float attackStateCounter;

    //Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
    AudioSource playerAudio;

	// Timers
	public float temp;
	public float tempDamage;

    // Control player
    [Header("Control")]
    private EnemyController EnemyController;
    private Rigidbody rigidBody;

    // Animations
    Animator anim;

	// Use this for initialization
	void Start ()
    {
		setAwake ();                                            // Call the setAwake function
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch (state) {
			case PlayerStates.AWAKE:
				AwakeBehaviour();
				break;
			case PlayerStates.IDLE:
				IdleBehaviour();
				break;
            case PlayerStates.ATTACK_10:
                Attack10Behaviour();
                break;
            case PlayerStates.DAMAGED:
				DamagedBehaviour();
				break;
			case PlayerStates.DEAD:
				DeadBehaviour();
				break;
		}
	}

	// Behaviours
	private void AwakeBehaviour()
	{
		setIdle ();     // Initalization
	}

	private void IdleBehaviour()
    {
        ActivationControlEnemy();                              // Activates the controls of the player, after the previous attack deactivation 
    }

    private void Attack10Behaviour()
    {
        attackStateCounter -= Time.deltaTime;       // Starts the countdown after the attack has been done

        if (attackStateCounter <= 0) setIdle();     // Goes back to setIdle if the player has not attack for a small amount of time
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
       
		currentHealth = maxHealth;                              // Sets the player health to the value of maxHealth that you indicated

        anim = GetComponent<Animator>();

        state = PlayerStates.AWAKE;                             // Cals the AWAKE state
	}

	public void setIdle()
    {
        Debug.Log("Idle");

        state = PlayerStates.IDLE;      // Calls the IDLE state
	}

    public void setAttack()
    {

        anim.SetTrigger("Attack10");        // Plays the attack animation

        damageDealt = 10;                   // Sets the amount of damage that the player does with this attack

        attackStateCounter = 0.5f;          // Sets the countdown value to return to idle state

        DeactivationControlEnemy();        // Deactivate the controls's player, so the player cannot move while attacking

        state = PlayerStates.ATTACK;     // Goes to the attack10 state
    }

    public void setDamaged(int damage)
	{

		temp = tempDamage;

		currentHealth -= damage;                // Applies the damage recieved

		if (currentHealth <= 0) setDead ();     // Calls the setDead function if the player has died
		else state = PlayerStates.DAMAGED;      // If the player is still alive, calls the DAMAGED state
	}

	public void setDead()
	{
        DeactivationControlEnemy();            // Deactivate the controls of the plauer
		currentHealth = 0;                      // Sets the health to 0

        anim.SetTrigger("Die");

		state = PlayerStates.DEAD;              // Calls the DEAD state
	}

    public void ActivationControlEnemy()
    {
        // TODO: Review Activation and Deactivaction. Not working proplerly
        rigidBody.isKinematic = false;           // Sets the rigidbody of the player to kinematic mode, no longer recieves forces
        EnemyController.enabled = true;        // Activate the playerController script, so the player can move
    }

    public void DeactivationControlEnemy()
    {
        // TODO: Review Activation and Deactivaction. Not working proplerly
        //rigidBody.isKinematic = true;           // Sets the rigidbody of the player to kinematic mode, no longer recieves forces
        EnemyController.enabled = false;       // Deactivate the playerController script, so the player can not move
    }
}
