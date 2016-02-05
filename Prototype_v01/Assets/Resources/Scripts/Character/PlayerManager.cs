using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	// States of the player
	public enum PlayerStates {AWAKE, IDLE, DAMAGED, DEAD, VICTORY}
	[Header("States")]
	public PlayerStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

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

	// Use this for initialization
	void Start () {

        playerController = GetComponent<PlayerController>();    // Gets the PlayerController script from the GameObject
        rigidBody = GetComponent<Rigidbody>();                  // Gets the RigidBody from the GameObject

        currentHealth = maxHealth;

		setAwake ();                                            // Call the setAwake function

        flashColor = new Color(1f, 0f, 0f, 0.1f);               // Sets the color values for the damageImage
    }
	
	// Update is called once per frame
	void Update () {

		switch (state) {
			case PlayerStates.AWAKE:
				AwakeBehaviour();
				break;
			case PlayerStates.IDLE:
				IdleBehaviour();
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
        ActivationControlPlayer();      // Calls the ActivationControlPlayer function

		currentHealth = maxHealth;

		state = PlayerStates.AWAKE;     // Cals the AWAKE state
	}

	public void setIdle()
    {
        damageImage.enabled = false;    // Deactivation of the damageImage
        state = PlayerStates.IDLE;      // Calls the IDLE state
	}

	public void setDamaged(int damage)
	{
        damageImage.enabled = true;             // Activation of the damage image
        damageImage.color = flashColor;         // Sets the color for the damageImage

		temp = tempDamage;

		currentHealth -= damage;                // Applies the damage recieved

        playerAudio.Play();

        healthSlider.value = currentHealth;     // Sets the value of the slider from the currentHealth of the player

		if (currentHealth <= 0) setDead ();     // Calls the setDead function if the player has died
		else state = PlayerStates.DAMAGED;      // If the player is still alive, calls the DAMAGED state
	}

	public void setDead()
	{
        DeactivationControlPlayer();            // Deactivate the controls of the plauer
		currentHealth = 0;                      // Sets the health to 0

		state = PlayerStates.DEAD;              // Calls the DEAD state
	}

	public void setVictory()
	{
		currentHealth = 0;

		state = PlayerStates.VICTORY;           // Calls the VICTORY state
	}

    public void ActivationControlPlayer()
    {
        playerController.enabled = true;        // Activate the playerController script, so the player can move
    }

    public void DeactivationControlPlayer()
    {
        rigidBody.isKinematic = true;           // Sets the rigidbody of the player to kinematic mode, no longer recieves forces
        playerController.enabled = false;       // Deactivate the playerController script, so the player can not move
    }
}
