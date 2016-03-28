using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

	// States of the player

	public enum PlayerStates {AWAKE, IDLE, DAMAGED, STUNNED, DEAD, VICTORY}

	[Header("States")]
	public PlayerStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

    // Controller
    [Header("Controller")]
    public float moveHorizontal;                    // Variable that gets the horizontal axis value.
    public float moveVertical;                      // Variable that gets the vertical axis value.
    private Vector3 movement;                       // Vector 3 with the values of the movement.

    // Movement
    [Header("Movement")]
    public float turnSmoothing;                     // A smoothing value for turning the player.
    public float speedDampTime;                     // The damping for the speed parameter.

    // Damage
    [Header("Attacks & Habilities")]
    public bool sword10Active;                      // Bool that allows to use the sword10 ability.
    //public bool sword20Active;                      // Bool that allows to use the sword20 ability.
    public bool chain10Active;                      // Bool that allows to use the chain10 ability.

    //Stunned
    [Header("Stunned")]
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
    public Image swordSprite;
    public Image chainSprite;
    public Image sword10Sprite;
    public Image sword20Sprite;
    public Image chain10Sprite;
	public Color flashColor;                        // The color of the damageImage.

	// Timers
    [Header("Timers")]
    //TODO: review the temp. What does?
	public float temp;                      
	public float tempDamage;                        // Counter that determinates how much time the player has to be in the DAMAGED state.
    public float attackStateCounter;                // Auxiliar variable that says how much time the player has to be in the ATTACKXX state. It gets the value from the different counters of each attack.

    // Control player
    [Header("Control player")]
    public GameObject sword;
    public Material swordMaterial;                  // Material from the sword. TODO: make it work.
    public BoxCollider attack10Collider;            // Gets the attackAction collider from the AttackAction child's player.
    public SphereCollider sword10Collider;          // Gets the player sword10Collider.
    public SphereCollider chain01Collider;          // Gets the player chain01Collider.
    public SphereCollider attack01Collider;		    // Gets the attack01Action collider from the attack01Collider child's player.
    public SphereCollider playerSphereCollider;           // Gets the player spherecollider.
    public CapsuleCollider playerCapsuleCollider;         // Gets the player capsulecollider.
    public bool godMode;
    public float attack01ColliderRadius;			// Auxiliar variable that sets the radius of the attack01Collider
    private PlayerAttack playerAttack;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    ChainAnimTrigger chainTransition;

    // Animations
    Animator anim;                                  // The animator component from the player.

	// Use this for initialization
	void Start ()
    {
		setAwake ();                                // Call the setAwake function.
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        switch(state)
        {
            case PlayerStates.IDLE:
                IdleBehaviour();
                break;
            case PlayerStates.DAMAGED:
                DamagedBehaviour();
                break;
        }
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.G)) godMode = !godMode;        // Changes between godmode and normal mode whenever the G key is pressed. G for God
        if (godMode)
        {
            playerSphereCollider.enabled = false;                         // Sets the sphereCollider from the player to false.
            playerCapsuleCollider.enabled = false;                        // Sets the capsuleCollider from the player to false.
            playerController.rigidBody.useGravity = false;                           // Deactivates the gravity from the player. Can no longer falls.
        }
        else
        {
            playerSphereCollider.enabled = true;                         // Sets the sphereCollider from the player to false.
            playerCapsuleCollider.enabled = true;                        // Sets the capsuleCollider from the player to false.
            playerController.rigidBody.useGravity = true;                            // Activates the gravity from the player. Can fall.
        }

		switch (state)
        {
			case PlayerStates.AWAKE:
				AwakeBehaviour();
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

        // TODO: Review the whole dash behaviour
		if (DashResistanceSlider.value < 100) DashResistanceSlider.value ++;        // Start gaining value when the current DashResistanceSlider.value falls from 100.
		if (currentHealth <= 10) 
		{
			playerAudio.clip = lowHpClip;
			playerAudio.Play();  
		}
	}

	// Behaviours
	private void AwakeBehaviour()
	{
		setIdle ();                                                                 // Initalization.
	}

	private void IdleBehaviour()
    {
        //TODO: Re-do the animation behaviour
        
        if (Input.GetKey(KeyCode.W))
        {
            playerController.MoveForward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.S))
        {
            playerController.MoveBackward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.D)) playerController.RotateRight();
        if (Input.GetKey(KeyCode.A)) playerController.RotateLeft();
        /*
        playerController.ControllerAction(playerController.playerSpeed);

        playerController.MovementManagement(moveHorizontal, moveVertical);

        playerController.Animating(moveHorizontal, moveVertical);
        */
        if (currentHealth <= 0) setDead();

        if(attackStateCounter >= 0)
        {
            attackStateCounter -= Time.deltaTime;
            playerController.transform.position += playerController.transform.forward * playerController.playerDisplacementSpeed * Time.deltaTime;
            //playerController.rigidBody.isKinematic = false;
        }
        else
        {
            attack10Collider.enabled = false;
            attack01Collider.enabled = false;
            sword10Collider.enabled = false;
            //playerController.rigidBody.isKinematic = true;

            if (Input.GetMouseButtonDown(0)) playerAttack.Attack10(playerAttack.damageAttack10);                                                         // Calls the setAttack10 function if mouse left button is pressed.
            else if (Input.GetMouseButtonDown(1)) playerAttack.Attack01(playerAttack.damageAttack01);                                                    // Calls the setAttack01 function if mouse right button is pressed.
		    else if (Input.GetKeyDown(KeyCode.Alpha1) && sword10Active) playerAttack.Sword10(playerAttack.damageSword10);                                // Calls the setSword10 function if the 1 keypad is pressed, and if is not in chain mode.
            else if (Input.GetKeyDown(KeyCode.Alpha2) && chain10Active) playerAttack.Chain01(playerAttack.damageChain01);                                // If the chain mode is active, goes to the setChain01.
            else if (Input.GetKeyDown(KeyCode.LeftShift)  && (DashResistanceSlider.value >= resistancePerDash)) playerAttack.Dash();        // Calls the setDash function if left shift key is pressed.
        }
        
    }

    private void DamagedBehaviour()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            playerController.MoveForward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.S))
        {
            playerController.MoveBackward(playerController.playerSpeed);
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("IsWalking", false);
        if (Input.GetKey(KeyCode.D)) playerController.RotateRight();
        if (Input.GetKey(KeyCode.A)) playerController.RotateLeft();
        /*
        playerController.ControllerAction(playerController.playerSpeed);

        playerController.MovementManagement(moveHorizontal, moveVertical);

        playerController.Animating(moveHorizontal, moveVertical);
        */
        if (attackStateCounter >= 0)
        {
            attackStateCounter -= Time.deltaTime;
            playerController.transform.position += playerController.transform.forward * playerController.playerDisplacementSpeed * Time.deltaTime;
            playerController.rigidBody.isKinematic = false;
        }
        else
        {
            attack10Collider.enabled = false;
            attack01Collider.enabled = false;
            sword10Collider.enabled = false;
            playerController.rigidBody.isKinematic = true;

            if (Input.GetMouseButtonDown(0)) playerAttack.Attack10(playerAttack.damageAttack10);                                                         // Calls the setAttack10 function if mouse left button is pressed.
            else if (Input.GetMouseButtonDown(1)) playerAttack.Attack01(playerAttack.damageAttack01);                                                    // Calls the setAttack01 function if mouse right button is pressed.
		    else if (Input.GetKeyDown(KeyCode.Alpha1) && sword10Active) playerAttack.Sword10(playerAttack.damageSword10);                                // Calls the setSword10 function if the 1 keypad is pressed, and if is not in chain mode.
            else if (Input.GetKeyDown(KeyCode.Alpha2) && chain10Active) playerAttack.Chain01(playerAttack.damageChain01);                                // If the chain mode is active, goes to the setChain01.
            else if (Input.GetKeyDown(KeyCode.LeftShift)  && (DashResistanceSlider.value >= resistancePerDash)) playerAttack.Dash();        // Calls the setDash function if left shift key is pressed.
        }

        temp -= Time.deltaTime;

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, tempDamage * Time.deltaTime);                    // Sets the difumination for the damageImage

        if (temp <= 0) setIdle();                                                            // If the player has not been attacked for a while, goes back to setIdle function
	}

    private void StunnedBehaviour()
    {
        timeStunned -= Time.deltaTime;

        if (timeStunned <= 0) setIdle();
    }

    private void DeadBehaviour()
	{
        // TODO: What happens when the player die.
	}

	private void VictoryBehaviour()
	{
        // TODO: WHat happens when the player wins.
	}

	// Sets
	public void setAwake()
    { 
		currentDashResistance = maxDashResistance;

        currentHealth = maxHealth;                          		        // Sets the player health to the value of maxHealth that you indicated.

		sword10Active = false;                                		        // Sets the sword10Active bool to false by default.
        //sword20Active = false;                                		    // Sets the sword20Active bool to false by default.
        chain10Active = false;                                		        // Sets the chain10Active bool to false by default.

        playerSphereCollider = GetComponent<SphereCollider>();
        playerCapsuleCollider = GetComponent<CapsuleCollider>();
        attack10Collider = GetComponentInChildren<BoxCollider>();      	        // Gets the BoxCollider of the PlaceHolder_Sword children.
		attack01ColliderRadius = attack01Collider.radius;   

        playerAudio = GetComponent<AudioSource>();          		        // Gets the component AudioSource from the player.

        timeStunned = timeStunnedIni;

        flashColor = new Color(1.0f, 0.0f, 0.0f, 0.1f);           		    // Sets the color values for the damageImage.
        swordSprite.enabled = true;
        chainSprite.enabled = false;
        sword10Sprite.enabled = false;
        sword20Sprite.enabled = false;
        chain10Sprite.enabled = false;

        swordMaterial = GetComponent<Renderer>().material;
        playerAttack = GetComponent<PlayerAttack>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        chainTransition = GetComponentInChildren<ChainAnimTrigger>();
        anim = GetComponent<Animator>();

        state = PlayerStates.AWAKE;                         		        // Cals the AWAKE state.
	}

	public void setIdle()
    {
        //Debug.Log("Idle");

		attack10Collider.enabled = false;                       // Deactivates the collider of the attack10 attack (sword).
		attack01Collider.enabled = false;                       // Deactivates the collider of the chain01 hability (chain).
        attack01Collider.radius = attack01ColliderRadius;       // Gives the chain01Collider it's previous radius value.
        sword10Collider.enabled = false;
        playerSphereCollider.enabled = true;
        playerCapsuleCollider.enabled = true;

        chainTransition.chainAnim = false;

        playerController.rigidBody.isKinematic = false;         // Deactivates the isKinematic bool of the rigidbody.
        temp = 0.0f;

        damageImage.enabled = false;                            // Deactivation of the damageImage.

        state = PlayerStates.IDLE;                              // Calls the IDLE state.

        timeStunned = timeStunnedIni;
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

        playerSphereCollider.enabled = false;                   // Sets the sphereCollider from the player to false.
        playerCapsuleCollider.enabled = false;                  // Sets the capsuleCollider from the player to false.

        anim.SetTrigger("Die");                                 // Plays the die animation.

        playerAudio.clip = deathClip;                           // Plays the hurt sound when you die.
        playerAudio.Play();

		state = PlayerStates.DEAD;                              // Calls the DEAD state.
	}

	public void setVictory()
	{
		state = PlayerStates.VICTORY;                           // Calls the VICTORY state.
	}

    // Functions
}