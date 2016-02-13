using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

	// States of the player
	public enum PlayerStates {AWAKE, IDLE, ATTACK_10, ATTACK_01, SLASH, DASH, DAMAGED, DEAD, VICTORY}
	[Header("States")]
	public PlayerStates state;

	public bool SlashActive;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

    // Controller
    [Header("Controller")]
    public int playerSpeed;                         // The speed of the player.
    public float moveHorizontal;                    // Variable that gets the horizontal axis value.
    public float moveVertical;                      // Variable that gets the vertical axis value.
    private Vector3 movement;                       // Vector 3 with the values of the movement.

    // Movement
    [Header("Movement")]
    public float turnSmoothing;                     // A smoothing value for turning the player.
    public float speedDampTime;                     // The damping for the speed parameter.

    // Damage
    [Header("Attack % Spells")]
    public int attackDamage;                        // Auxiliar variable that gets the value of the differents attacks. After, is used to apply the damage to the enemy.
    public int attack10;                            // Variable with the damage of the attack10.
    public int attack01;                            // Variable with the damage of the attack01.
    public int slash;                               // Variable with the damage of the slash.
    public GameObject sword;                        // Gets the sword gameobject from the plaer.

    // Dash
    [Header("Dash")]
    public float speedDash;                         // The speed wich the player makes a dash.

    //Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
    AudioSource playerAudio;

	// UI Player
	[Header("UI")]
	public Slider healthSlider;                     // It shows the health bar.
	public Image damageImage;                       // The UI image that shows when the player gets hit.
	public Color flashColor;                        // The color of the damageImage.

	// Timers
    [Header("Timers")]
	public float temp;
	public float tempDamage;                        // Counter that determinates how much time the player has to be in the DAMAGED state.
    public float tempAttack10;                      // Counter that reflects how much the animation of the attack10 longs.
    public float tempAttack01;                      // Counter that reflects how much the animation of the attack01 longs.
    public float tempSlash;                         // Counter that reflects how much the animation of the slash longs.
    public float tempDash;                          // Counter that determinates how much time the player has to be in the DASH state.
    public float attackStateCounter;                // Auxiliar variable that says how much time the player has to be in the ATTACKXX state. It gets the value from the different counters of each attack.

    // Control player
    [Header("Control")]
    private Rigidbody rigidBody;                    // The rigidbody from the player.

    // Animations
    Animator anim;                                  // The animator component from the player.

	// Use this for initialization
	void Start ()
    {
		setAwake ();                                // Call the setAwake function.
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch (state)
        {
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
            case PlayerStates.SLASH:
                SlashBehaviour();
                break;
            case PlayerStates.DASH:
                DashBehaviour();
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
		setIdle ();                                             // Initalization.
	}

	private void IdleBehaviour()
    {
        ControllerAction(playerSpeed);                          // Calls the ControllerAction function, and give it a speed value for the player.

        MovementManagement(moveHorizontal, moveVertical);       // Calls the MovementManagement function.

        Animating(moveHorizontal, moveVertical);                // Calls the Animating function.

        if (Input.GetMouseButtonDown(0)) setAttack10();         // Calls the setAttack10 function if mouse left button is pressed.
        else if (Input.GetMouseButtonDown(1)) setAttack01();    // Calls the setAttack01 function if mouse right button is pressed.

		if (Input.GetKeyDown(KeyCode.Alpha1) && SlashActive) setSlash();

        if (Input.GetKeyDown(KeyCode.LeftShift)) setDash();     // Calls the setDash function if left shift key is pressed.
    }

    private void Attack10Behaviour()
    {
        attackStateCounter -= Time.deltaTime;                   // Starts the countdown after the attack has been done.

        if (attackStateCounter <= 0) setIdle();                 // Goes back to setIdle if the player has not attack for a small amount of time.
    }

    private void Attack01Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void SlashBehaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void DashBehaviour()
    {
        tempDash -= Time.deltaTime;

        if (tempDash <= 0) setIdle();
    }

    private void DamagedBehaviour()
	{
        ControllerAction(playerSpeed);

        MovementManagement(moveHorizontal, moveVertical);

        Animating(moveHorizontal, moveVertical);

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
        currentHealth = maxHealth;                      // Sets the player health to the value of maxHealth that you indicated.

		SlashActive = false;

        playerAudio = GetComponent<AudioSource>();      // Gets the component AudioSource from the player.

        flashColor = new Color(1f, 0f, 0f, 0.1f);       // Sets the color values for the damageImage.

        rigidBody = GetComponent<Rigidbody>();          // Gets the RigidBody from the GameObject.

        anim = GetComponent<Animator>();

        state = PlayerStates.AWAKE;                     // Cals the AWAKE state.
	}

	public void setIdle()
    {
        Debug.Log("Idle");

        rigidBody.isKinematic = false;

        tempDash = 0.5f;                                // Resets the tempDash counter.

        damageImage.enabled = false;                    // Deactivation of the damageImage.

        state = PlayerStates.IDLE;                      // Calls the IDLE state.
	}

    public void setAttack10()
    {
        Debug.Log("Attack10");

        ForcesDeactivation();

        AttackAction(attack10, tempAttack10);           // Calls the AttackAction function, and give it the attack10 variable, and the tempAttack10 variable.

        anim.SetTrigger("Attack10");                    // Plays the attack10 animation.

        state = PlayerStates.ATTACK_10;                 // Goes to the attack10 state.
    }

    public void setAttack01()
    {
        Debug.Log("Attack01");

        ForcesDeactivation();

        AttackAction(attack01, tempAttack01);

        anim.SetTrigger("Attack01");

        state = PlayerStates.ATTACK_01;
    }

    public void setSlash()
    {
        Debug.Log("Slash");

        ForcesDeactivation();

        AttackAction(slash, tempSlash);

        anim.SetTrigger("AttackHab01");

        state = PlayerStates.SLASH;
    }

    public void setDash()
    {
        Debug.Log("Dash");

        anim.SetTrigger("IsDashing");                           // Plays the dash animation.

        rigidBody.AddForce(transform.forward * speedDash);      // Adds a force to the rigidbody of the player, in order of doing a dash.

        state = PlayerStates.DASH;                              // Goes to the dash state.
    }

    public void setDamaged(int damage)
	{
        damageImage.enabled = true;                             // Activation of the damage image.
        damageImage.color = flashColor;                         // Sets the color for the damageImage.

		temp = tempDamage;

		currentHealth -= damage;                                // Applies the damage recieved.

        playerAudio.Play();                                     // Plays the hurt sound when the player gets hit.

        healthSlider.value = currentHealth;                     // Sets the value of the slider from the currentHealth of the player.

		if (currentHealth <= 0) setDead ();                     // Calls the setDead function if the player has died.
		else state = PlayerStates.DAMAGED;                      // If the player is still alive, calls the DAMAGED state.
	}

	public void setDead()
	{
		currentHealth = 0;                                      // Sets the health to 0.

        anim.SetTrigger("Die");                                 // Plays the die animation.

        playerAudio.clip = hurtClip;                            // Plays the hurt sound when you get hit.
        playerAudio.Play();

		state = PlayerStates.DEAD;                              // Calls the DEAD state.
	}

	public void setVictory()
	{
		state = PlayerStates.VICTORY;                           // Calls the VICTORY state.
	}

    // Functions 
    public void ControllerAction(int speed)
    {
        // TODO: Change the controller behaviour. Use transform.positions insted of rigidbody.velocity.

        moveHorizontal = Input.GetAxis("Horizontal");                       // Takes the horizontal axis.
        moveVertical = Input.GetAxis("Vertical");                           // Takes the vertical axis.

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);         
        rigidBody.velocity = movement * speed;

        if (moveHorizontal != 0 && moveVertical != 0)
        {
            rigidBody.velocity *= 0.8f;
        }

    }

    public void MovementManagement(float horizontal, float vertical)
    {
        //If there is some axis input...
        if (horizontal != 0 || vertical != 0)
        {
            // ...set the players rotation and set the speed paramter to 5.5f
            Rotating(horizontal, vertical);
        }
    }

    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);

        // Create a rotation based on this new vector assuming that up is the global axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a reotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rigidBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        rigidBody.MoveRotation(newRotation);
    }

    void Animating(float horizontal, float vertical)
    {
        bool walking = horizontal != 0f || vertical != 0f;
        anim.SetBool ("IsWalking", walking);
    }

    void AttackAction(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }

    void ForcesDeactivation()
    {
        rigidBody.isKinematic = true;                               // Sets the isKinematic option to true.  
    }
}