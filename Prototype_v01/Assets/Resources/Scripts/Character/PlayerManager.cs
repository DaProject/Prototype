/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	// States of the player
	public enum PlayerStates {AWAKE, ACTIVE, DAMAGED, DEAD, VICTORY}
	[Header("STATES")]
	public PlayerStates state;

	// Health
	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

	// UI Player
	[Header("UI")]
	public Slider healthSlider;
	public Image damageImage;
	public Color flashColor;

	// Timers
	public float temp;
	public float tempDamage;

	// Control player
	private PlayerController playerController;

	// Use this for initialization
	void Start () {

		playerController = GetComponent<PlayerController> ();

		// Deactivation of the image of damage
		damageImage.enabled = false;

		setAwake ();

		currentHealth = maxHealth;
	
	}
	
	// Update is called once per frame
	void Update () {

		switch (state) {
			case PlayerStates.AWAKE:
				AwakeBehaviour();
				break;
			case PlayerStates.ACTIVE:
				ActiveBehaviour();
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
		// Initialization
		setActive ();
	}
	private void ActiveBehaviour()
	{
		
	}
	private void DamagedBehaviour()
	{
		
		// TEMPORIZADOR HACIA ATRAS
		temp -= Time.deltaTime;
		
		if (temp <= 0) {
			setActive();
		}
		
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
		// Activation of the controls of the player
		ActivateControlPlayer ();

		currentHealth = maxHealth;

		state = PlayerStates.AWAKE;
	}

	public void setActive()
	{
		state = PlayerStates.ACTIVE;
	}

	public void setDamaged(int damage)
	{
		//Activation of the damage image
		damageImage.enabled = true;

		temp = tempDamage;

		currentHealth -= damage;

		if (healthSlider.value <= 0) setDead ();
		else state = PlayerStates.DAMAGED;
	}

	public void setDead()
	{
		// Deactivation of the control's player
		DeactivateControlPlayer ();

		currentHealth = 0;

		state = PlayerStates.DEAD;
	}

	public void setVictory()
	{
		//Deactivation of the control's player
		DeactivateControlPlayer();

		currentHealth = 0;

		state = PlayerStates.VICTORY;
	}

	// Activation of the control's player
	public void ActivateControlPlayer()
	{
		playerController.enabled = true;
	}

	public void DeactivateControlPlayer()
	{
		playerController.enabled = false;
	}
}
*/