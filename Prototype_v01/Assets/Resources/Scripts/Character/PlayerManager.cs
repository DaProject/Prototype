using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

	// States of the player

	public enum PlayerStates {AWAKE, IDLE, ATTACK_10, ATTACK_01, SWORD_10, SWORD_20, SWORD_30, SWORD_40, CHAIN_01, CHAIN_02, CHAIN_03, CHAIN_04, SLASH, DASH, DAMAGED, STUNNED, DEAD, VICTORY}

	[Header("States")]
	public PlayerStates state;

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
    [Header("Attacks & Habilities")]
    public int attackDamage;                        // Auxiliar variable that gets the value of the differents attacks. After, is used to apply the damage to the enemy.
    public int attack10;                            // Variable with the damage of the attack10 (sword).
    public int sword10;                             // Variable with the damage of the sword10 (sword hability).
    public int sword20;                             // Variable with the damage of the sword20 (sword hability).
    public int sword30;                             // Variable with the damage of the sword30 (sword hability).
    public int sword40;                             // Variable with the damage of the sword40 (sword hability).
    public int attack01;                            // Variable with the damage of the attack01 (chain).
    public int chain01;                             // Variable with the damage of the chain01 (chain hability).
    public int chain02;                             // Variable with the damage of the chain02 (chain hability).
    public int chain03;                             // Variable with the damage of the chain03 (chain hability).
    public int chain04;                             // Variable with the damage of the chain04 (chain hability).
    public int speedAttack10;                       // The time the animation has to long.
    public int speedSword10;                        // The time the animation has to long.
    public int speedSword20;                        // The time the animation has to long.
    public int speedSword30;                        // The time the animation has to long.
    public int speedSword40;                        // The time the animation has to long.
    public int speedAttack01;                       // The time the animation has to long.
    public int speedChain01;                        // The time the animation has to long.
    public int speedChain02;                        // The time the animation has to long.
    public int speedChain03;                        // The time the animation has to long.
    public int speedChain04;                        // The time the animation has to long.
    public int slash;                               // Variable with the damage of the slash.
    public bool chainMode;                          // Bool that says the attack mode the player is in. Sword or chain.
    public bool slashActive;                        // Bool that allows to use the slash hability.
    public float timeStunned;
    public float timeStunnedIni;

    // Dash
    [Header("Dash")]
    public float speedDash;                         // The speed wich the player makes a dash.
	public int maxDashResistance;
	public int currentDashResistance;
	public int resistancePerDash;
	public Slider DashResistanceSlider;

    //Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
	public AudioClip lowHpClip;
	public AudioClip deathClip;
	public AudioClip swordSwipeClip;
    AudioSource playerAudio;

	// UI Player
	[Header("UI")]
	public Slider healthSlider;                     // It shows the health bar.
	public Image damageImage;                       // The UI image that shows when the player gets hit.
	public Color flashColor;                        // The color of the damageImage.

	// Timers
    [Header("Timers")]
	private float temp;
	public float tempDamage;                        // Counter that determinates how much time the player has to be in the DAMAGED state.
    public float tempAttack10;                      // Counter that reflects how much the animation of the attack10 longs.
    public float tempSword10;                       // Counter that reflects how much the animation of the sword10 logns.
    public float tempSword20;                       // Counter that reflects how much the animation of the sword20 logns.
    public float tempSword30;                       // Counter that reflects how much the animation of the sword30 logns.
    public float tempSword40;                       // Counter that reflects how much the animation of the sword40 logns.
    public float tempAttack01;                      // Counter that reflects how much the animation of the attack01 longs.
    public float tempChain01;                       // Counter that reflects how much the animation of the chain10 logns.
    public float tempChain02;                       // Counter that reflects how much the animation of the chain02 logns.
    public float tempChain03;                       // Counter that reflects how much the animation of the chain03 logns.
    public float tempChain04;                       // Counter that reflects how much the animation of the chain04 logns.
    public float tempSlash;                         // Counter that reflects how much the animation of the slash longs.
    public float tempDash;                          // Counter that determinates how much time the player has to be in the DASH state.
    private float attackStateCounter;               // Auxiliar variable that says how much time the player has to be in the ATTACKXX state. It gets the value from the different counters of each attack.

    // Control player
    [Header("Control")]
    public GameObject attack01Gameobject;           // Gets the Attack01 gameobject from the Attack01 gameobjects child's player.
	public SphereCollider chain01Collider;			// Gets the chain01Action collider from the chain01Collider child's player.
	public BoxCollider attackAction;                // Gets the attackAction collider from the AttackAction child's player.
    private Rigidbody rigidBody;                    // The rigidbody from the player.
    private SphereCollider sphereCollider;          // Gets the player collider.
    public float chain01ColliderRadius;				// Auxiliar variable that sets the radius of the chain01Collider
    public float attack01GameobjectRotation;      // Auxiliar variable that sets the rotation of the attack01Gameobject.

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
            case PlayerStates.SWORD_10:
                Sword10Behaviour();
                break;
            case PlayerStates.SWORD_20:
                Sword20Behaviour();
                break;
            case PlayerStates.SWORD_30:
                Sword30Behaviour();
                break;
            case PlayerStates.SWORD_40:
                Sword40Behaviour();
                break;
            case PlayerStates.CHAIN_01:
                Chain01Behaviour();
                break;
            case PlayerStates.CHAIN_02:
                Chain02Behaviour();
                break;
            case PlayerStates.CHAIN_03:
                Chain03Behaviour();
                break;
            case PlayerStates.CHAIN_04:
                Chain04Behaviour();
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
            case PlayerStates.STUNNED:
                StunnedBehaviour();
                break;
            case PlayerStates.DEAD:
				DeadBehaviour();
				break;
			case PlayerStates.VICTORY:
				VictoryBehaviour();
				break;
		}

		if (DashResistanceSlider.value < 100) DashResistanceSlider.value ++; 
		if (currentHealth <= 10) 
		{
			playerAudio.clip = lowHpClip;
			playerAudio.Play();  
		}
	}

	// Behaviours
	private void AwakeBehaviour()
	{
		setIdle ();                                             // Initalization.
	}

	private void IdleBehaviour()
    {
        ControllerAction(playerSpeed);                                              // Calls the ControllerAction function, and give it a speed value for the player.

        MovementManagement(moveHorizontal, moveVertical);                           // Calls the MovementManagement function.

        Animating(moveHorizontal, moveVertical);                                    // Calls the Animating function.

        if (Input.GetKeyDown(KeyCode.Q)) chainMode = !chainMode;

        if (Input.GetMouseButtonDown(0)) setAttack10();                             // Calls the setAttack10 function if mouse left button is pressed.
        else if (Input.GetMouseButtonDown(1)) setAttack01();                        // Calls the setAttack01 function if mouse right button is pressed.

		if (Input.GetKeyDown(KeyCode.Alpha1) && !chainMode) setSword10();           // Cals the setSword10 function if the 1 keypad is pressed, and if is not in chain mode.
        else if (Input.GetKeyDown(KeyCode.Alpha1) && chainMode) setChain01();       // If the chain mode is active, goes to the setChain01.

        if (Input.GetKeyDown(KeyCode.Alpha2) && !chainMode) setSword20();           // Cals the setSword20 function if the 1 keypad is pressed, and if is not in chain mode.
        else if (Input.GetKeyDown(KeyCode.Alpha2) && chainMode) setChain02();       // If the chain mode is active, goes to the setChain02.

        if (Input.GetKeyDown(KeyCode.Alpha3) && !chainMode) setSword30();           // Cals the setSword30 function if the 1 keypad is pressed, and if is not in chain mode.
        else if (Input.GetKeyDown(KeyCode.Alpha3) && chainMode) setChain03();       // If the chain mode is active, goes to the setChain03.

        if (Input.GetKeyDown(KeyCode.Alpha4) && !chainMode) setSword40();           // Cals the setSword40 function if the 1 keypad is pressed, and if is not in chain mode.
        else if (Input.GetKeyDown(KeyCode.Alpha4) && chainMode) setChain04();       // If the chain mode is active, goes to the setChain04.

        if (Input.GetKeyDown(KeyCode.LeftShift)  && (DashResistanceSlider.value >= resistancePerDash)) setDash();     // Calls the setDash function if left shift key is pressed.
    }

    private void Attack10Behaviour()
    {
        attackStateCounter -= Time.deltaTime;                   // Starts the countdown after the attack has been done.

        if (attackStateCounter <= 0) setIdle();                 // Goes back to setIdle if the player has not attack for a small amount of time.
    }

    private void Attack01Behaviour()
    {
        attack01Gameobject.transform.rotation = Quaternion.Euler(attack01Gameobject.transform.rotation.x, attack01GameobjectRotation -= 1, attack01Gameobject.transform.rotation.z);

        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Sword10Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Sword20Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Sword30Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Sword40Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Chain01Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

		chain01Collider.radius += 10*Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Chain02Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Chain03Behaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setIdle();
    }

    private void Chain04Behaviour()
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

        if (Input.GetMouseButtonDown(0)) setAttack10();         // Calls the setAttack10 function if mouse left button is pressed.
        else if (Input.GetMouseButtonDown(1)) setAttack01();    // Calls the setAttack01 function if mouse right button is pressed.

        if (Input.GetKeyDown(KeyCode.Alpha1) && slashActive) setSlash();

        if (Input.GetKeyDown(KeyCode.LeftShift)) setDash();     // Calls the setDash function if left shift key is pressed.

        temp -= Time.deltaTime;                                                                             // Backwards counter

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, tempDamage * Time.deltaTime);        // Sets the difumination for the damageImage

        if (temp <= 0) setIdle();                                                                           // If the player has not been attacked for a while, goes back to setIdle function
	}

    private void StunnedBehaviour()
    {
        ForcesDeactivation();

        timeStunned -= Time.deltaTime;

        if (timeStunned <= 0) setIdle();
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
		currentDashResistance = maxDashResistance;

        currentHealth = maxHealth;                          		// Sets the player health to the value of maxHealth that you indicated.

		slashActive = false;                                		// Sets the slashActive bool to false by default.
        chainMode = false;                                  		// Sword mode activade by default.

        sphereCollider = GetComponent<SphereCollider>();
		attackAction = GetComponentInChildren<BoxCollider>();      	// Gets the BoxCollider of the PlaceHolder_Sword children.
		chain01ColliderRadius = chain01Collider.radius;

        playerAudio = GetComponent<AudioSource>();          		// Gets the component AudioSource from the player.

        timeStunned = timeStunnedIni;

        flashColor = new Color(1f, 0f, 0f, 0.1f);           		// Sets the color values for the damageImage.

        rigidBody = GetComponent<Rigidbody>();              		// Gets the RigidBody from the GameObject.

        anim = GetComponent<Animator>();

        state = PlayerStates.AWAKE;                         		// Cals the AWAKE state.
	}

	public void setIdle()
    {
        //Debug.Log("Idle");

        sphereCollider.enabled = true;

		attackAction.enabled = false;                               // Deactivates the collider of the attack10 attack (sword).
		chain01Collider.enabled = false;                            // Deactivates the collider of the chain01 hability (chain).
        attack01Gameobject.SetActive(!attack01Gameobject);          // Deactivates the collider of the attack01 attack (chain).
        chain01Collider.radius = chain01ColliderRadius;             // Gives the chain01Collider it's previous radius value.
        attack01GameobjectRotation = attack01Gameobject.transform.rotation.y + 30;

        rigidBody.isKinematic = false;                              // Deactivates the isKinematic bool of the rigidbody.

        tempDash = 0.5f;                                            // Resets the tempDash counter.

        damageImage.enabled = false;                                // Deactivation of the damageImage.

        state = PlayerStates.IDLE;                                  // Calls the IDLE state.

        timeStunned = timeStunnedIni;
	}

    public void setAttack10()
    {
        Debug.Log("Attack10");

        //ForcesDeactivation();

        //AnimationControllerAction();

        rigidBody.AddForce(transform.forward * speedAttack10);

        Attack10Action(attack10, tempAttack10);           // Calls the AttackAction function, and give it the attack10 variable, and the tempAttack10 variable.

        anim.SetTrigger("Attack10");                    // Plays the attack10 animation.

		playerAudio.clip = swordSwipeClip;
		playerAudio.Play();

        state = PlayerStates.ATTACK_10;                 // Goes to the attack10 state.
    }

    public void setAttack01()
    {
        Debug.Log("Attack01");

        //ForcesDeactivation();

        Attack01Action(attack01, tempAttack01);

        rigidBody.AddForce(transform.forward * speedAttack01);

        anim.SetTrigger("Attack01");

		playerAudio.clip = swordSwipeClip;
		playerAudio.Play();

        state = PlayerStates.ATTACK_01;
    }

    public void setSword10()
    {
        Debug.Log("Sword10");

        Sword10Action(sword10, tempSword10);

        rigidBody.AddForce(transform.forward * speedSword10);

        anim.SetTrigger("Sword10");

        state = PlayerStates.SWORD_10;
    }

    public void setSword20()
    {
        Debug.Log("Sword20");

        Sword20Action(sword20, tempSword20);

        rigidBody.AddForce(transform.forward * speedSword20);

        anim.SetTrigger("Attack01");

        state = PlayerStates.SWORD_20;
    }

    public void setSword30()
    {
        Debug.Log("Sword30");

        Sword30Action(sword30, tempSword30);

        rigidBody.AddForce(transform.forward * speedSword30);

        anim.SetTrigger("Attack01");

        state = PlayerStates.SWORD_30;
    }

    public void setSword40()
    {
        Debug.Log("Sword40");

        Sword40Action(sword40, tempSword40);

        rigidBody.AddForce(transform.forward * speedSword40);

        anim.SetTrigger("Attack01");

        state = PlayerStates.SWORD_40;

    }

    public void setChain01()
    {
        Debug.Log("Chain01");

        Chain01Action(chain01, tempChain01);

        rigidBody.AddForce(transform.forward * speedChain01);

        anim.SetTrigger("Attack01");

        state = PlayerStates.CHAIN_01;
    }

    public void setChain02()
    {
        Debug.Log("Chain02");

        Chain02Action(chain02, tempChain02);

        rigidBody.AddForce(transform.forward * speedChain02);

        anim.SetTrigger("Attack01");

        state = PlayerStates.CHAIN_02;
    }

    public void setChain03()
    {
        Debug.Log("Chain03");

        Chain03Action(chain03, tempChain03);

        rigidBody.AddForce(transform.forward * speedChain03);

        anim.SetTrigger("Attack01");

        state = PlayerStates.CHAIN_03;
    }

    public void setChain04()
    {
        Debug.Log("Chain04");

        Chain04Action(chain04, tempChain04);

        rigidBody.AddForce(transform.forward * speedChain04);

        anim.SetTrigger("Attack01");

        state = PlayerStates.CHAIN_04;
    }

    public void setSlash()
    {
        //Debug.Log("Slash");

        //ForcesDeactivation();

        Attack10Action(slash, tempSlash);

        anim.SetTrigger("AttackHab01");

        state = PlayerStates.SLASH;
    }

    public void setDash()
    {
        //Debug.Log("Dash");

		DashResistanceSlider.value -= resistancePerDash;

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

		playerAudio.clip = hurtClip;
        playerAudio.Play();                                     // Plays the hurt sound when the player gets hit.

        healthSlider.value = currentHealth;                     // Sets the value of the slider from the currentHealth of the player.

		if (currentHealth <= 0) setDead ();                     // Calls the setDead function if the player has died.
		else state = PlayerStates.DAMAGED;                      // If the player is still alive, calls the DAMAGED state.
	}

    public void setStunned()
    {
        anim.SetTrigger("Stunned");

        state = PlayerStates.STUNNED;
    }


    public void setDead()
	{
		currentHealth = 0;                                      // Sets the health to 0.

        sphereCollider.enabled = false;

        anim.SetTrigger("Die");                                 // Plays the die animation.

        playerAudio.clip = deathClip;                            // Plays the hurt sound when you die.
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

        movement.Normalize();
        rigidBody.velocity = movement * speed;
    }
    
    public void AnimationControllerAction()
    {
        if (moveHorizontal >= 0) transform.Translate(Vector3.right * 5 * Time.deltaTime);
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
		//Quaternion rotPhase = Quaternion.AngleAxis (45.0f, Vector3.up);

		//newRotation *= rotPhase;

        // Change the players rotation to this new rotation.
		rigidBody.MoveRotation(newRotation);
    }

    void Animating(float horizontal, float vertical)
    {
        bool walking = horizontal != 0f || vertical != 0f;
        anim.SetBool ("IsWalking", walking);
    }

    void Attack10Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

		attackAction.enabled = true;                                // Activates the collider of the sword.
    }

    void Attack01Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

        attack01Gameobject.SetActive(attack01Gameobject);          // Activates the collider of the sword.
    }
    

    void Sword10Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

        attackAction.enabled = true;                                // Activates the collider of the sword.
    }

    void Sword20Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

        attackAction.enabled = true;                                // Activates the collider of the sword.
    }

    void Sword30Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }

    void Sword40Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }

    void Chain01Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.

		chain01Collider.enabled = true;
    }

    void Chain02Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }

    void Chain03Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }

    void Chain04Action(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;                                 // Sets the amount of damage that the player does with this attack.

        attackStateCounter = attackDuration;                        // Sets the amount of time that the player has to be in the attackXX state.
    }

    public void ForcesDeactivation()
    {
        rigidBody.isKinematic = true;                               // Sets the isKinematic option to true.  
    }
}