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
    NavMeshAgent nav;
    GameObject player;
    
    // Damage
    [Header("Attack")]
    public int attackDamage;
    public int attackMelee;
    public bool playerInRange;

    // Sounds
    [Header("Sounds")]
    public AudioClip hurtClip;
    AudioSource enemyAudio;

    // Timers
    [Header("Timers")]
    public float temp;
    public float tempDamage;
    public float tempAttackMelee;
    public float attackStateCounter;

    // Control enemy
    [Header("Control")]
    private Rigidbody rigidBody;

    // Animations
    Animator anim;

    // Scripts calls
    PlayerManager playerManager;

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
        anim.SetTrigger("Idle");
    }

    private void ActiveBehaviour()
    {
        ControllerAction();

        if (playerInRange) setAttack();
    }

    private void AttackBehaviour()
    {
        attackStateCounter -= Time.deltaTime;

        if (attackStateCounter <= 0) setActive();
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
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");    // Finds the gameobject with the tag "Player". Finds the player               // Gets the transform of the player

        playerManager = player.GetComponent<PlayerManager>();   // Gets the script PlayerManager of the player

        nav = GetComponent<NavMeshAgent>();                     // Gets the NavMeshAgent component

        playerInRange = false;

        enemyAudio = GetComponent<AudioSource>();

        rigidBody = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        state = EnemyStates.AWAKE;
    }

    public void setIdle()
    {

    }

    public void setActive()
    {
        anim.SetBool("Attack", false);

        state = EnemyStates.ACTIVE;
    }

    public void setAttack()
    {
        AttackAction(attackMelee, tempAttackMelee);

        anim.SetBool("Attack", true);

        anim.SetBool("Run", false);

        state = EnemyStates.ATTACK;
    }

    public void setDamaged()
    {

    }

    public void setDead()
    {

    }

    private void ControllerAction()
    {
        nav.SetDestination(player.transform.position);

        anim.SetBool("Run", true);
    }

    private void AttackAction(int damageDealt, float attackDuration)
    {
        attackDamage = damageDealt;

        attackStateCounter = attackDuration;
    }

    void  OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") playerInRange = false;
    }
}
