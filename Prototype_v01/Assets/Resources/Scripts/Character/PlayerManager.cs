using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	// States of the player
	public enum PlayerStates {AWAKE, IDLE, ATTACK_10, ATTACK_01, DAMAGED, DEAD, VICTORY}
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
    AudioSource playerAudio;
    public AudioClip hurtClip;

	// UI Player
	[Header("UI")]
	public Slider healthSlider;
	public Image damageImage;
	public Color flashColor;

	// Timers
	public float temp;
	public float tempDamage;

    // Control player
    [Header("Control")]
    private PlayerController playerController;
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
            case PlayerStates.ATTACK_01:
                Attack01Behaviour();
                break;
            case PlayerStates.DAMAGED:
				DamagedBehaviour();
				break;
			case PlayerStates.DEAD:
				DeadBehaviour();
				break;
			case PlayerStates.VICTORY:
				VictoryBehaviour();
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
        ActivationControlPlayer();                              // Activates the controls of the player, after the previous attack deactivation 

        if (Input.GetMouseButtonDown(0)) setAttack10();         // Goes to main attack if mouse left button is pressed
        else if (Input.GetMouseButtonDown(1)) setAttack01();    // Goes to off attack if mouse right button is pressed
    }

    private void Attack10Behaviour()
    {
        attackStateCounter -= Time.deltaTime;       // Starts the countdown after the attack has been done

        if (attackStateCounter <= 0) setIdle();     // Goes back to setIdle if the player has not attack for a small amount of time
    }

    private void Attack01Behaviour()
    {
        attackStateCounter -= Time.deltaTime;       // Starts the countdown after the attack has been done

        if (attackStateCounter <= 0) setIdle();     // Goes back to setIdle if the player has not attack for a small amount of time
    }

    private void DamagedBehaviour()
	{
		temp -= Time.deltaTime;                                                                             // Backwards counter

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, tempDamage * Time.deltaTime);        // Sets the difumination for the damageImage

        if (temp <= 0) setIdle();                                                                           // If the player has not been attacked for a while, goes back to setIdle function
	}
	private void DeadBehaviour()
	{

	}

	private void VictoryBehaviour()
	{

	}

	// Sets
	public void setAwake()
	{
        playerAudio = GetComponent<AudioSource>();              // Gets the component AudioSource from the player

        flashColor = new Color(1f, 0f, 0f, 0.1f);               // Sets the color values for the damageImage

        playerController = GetComponent<PlayerController>();    // Gets the PlayerController script from the GameObject
        rigidBody = GetComponent<Rigidbody>();                  // Gets the RigidBody from the GameObject

		currentHealth = maxHealth;                              // Sets the player health to the value of maxHealth that you indicated

        anim = GetComponent<Animator>();

        ActivationControlPlayer();                              // Calls the ActivationControlPlayer function

        state = PlayerStates.AWAKE;                             // Cals the AWAKE state
	}

	public void setIdle()
    {
        Debug.Log("Idle");

        damageImage.enabled = false;    // Deactivation of the damageImage

        state = PlayerStates.IDLE;      // Calls the IDLE state
	}

    public void setAttack10()
    {
        Debug.Log("Attack10");

        anim.SetTrigger("Attack10");        // Plays the attack animation

        damageDealt = 10;                   // Sets the amount of damage that the player does with this attack

        attackStateCounter = 0.5f;          // Sets the countdown value to return to idle state

        DeactivationControlPlayer();        // Deactivate the controls's player, so the player cannot move while attacking

        state = PlayerStates.ATTACK_10;     // Goes to the attack10 state
    }

    public void setAttack01()
    {
        Debug.Log("Attack01");

        anim.SetTrigger("Attack10");

        damageDealt = 5;

        attackStateCounter = 0.5f;

        DeactivationControlPlayer();

        state = PlayerStates.ATTACK_01;
    }

    public void setDamaged(int damage)
	{
        damageImage.enabled = true;             // Activation of the damage image
        damageImage.color = flashColor;         // Sets the color for the damageImage

		temp = tempDamage;

		currentHealth -= damage;                // Applies the damage recieved

        playerAudio.Play();                     // Plays the hurt sound when the player gets hit

        healthSlider.value = currentHealth;     // Sets the value of the slider from the currentHealth of the player

		if (currentHealth <= 0) setDead ();     // Calls the setDead function if the player has died
		else state = PlayerStates.DAMAGED;      // If the player is still alive, calls the DAMAGED state
	}

	public void setDead()
	{
        DeactivationControlPlayer();            // Deactivate the controls of the plauer
		currentHealth = 0;                      // Sets the health to 0

        anim.SetTrigger("Die");

        playerAudio.clip = hurtClip;            // Plays the hurt sound when you get hit
        playerAudio.Play();

		state = PlayerStates.DEAD;              // Calls the DEAD state
	}

	public void setVictory()
	{
		currentHealth = 0;

		state = PlayerStates.VICTORY;           // Calls the VICTORY state
	}

    public void ActivationControlPlayer()
    {
        // TODO: Review Activation and Deactivaction. Not working proplerly
        rigidBody.isKinematic = false;           // Sets the rigidbody of the player to kinematic mode, no longer recieves forces
        playerController.enabled = true;        // Activate the playerController script, so the player can move
    }

    public void DeactivationControlPlayer()
    {
        // TODO: Review Activation and Deactivaction. Not working proplerly
        rigidBody.isKinematic = true;           // Sets the rigidbody of the player to kinematic mode, no longer recieves forces
        playerController.enabled = false;       // Deactivate the playerController script, so the player can not move
    }
}
