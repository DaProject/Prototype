using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerController playerController;
    public PlayerAttack playerAttack;

    public float tempAttack10;                      // Counter that reflects how much the animation of the attack10 longs.
    public float tempSword10;                       // Counter that reflects how much the animation of the sword10 longs.
    public float tempSword20;                       // Counter that reflects how much the animation of the sword20 longs.
    public float tempAttack01;                      // Counter that reflects how much the animation of the attack01 longs.
    public float tempChain01;                       // Counter that reflects how much the animation of the chain10 longs.
    public float tempChain02;                       // Counter that reflects how much the animation of the chain02 longs.
    public float tempSlash;                         // Counter that reflects how much the animation of the slash longs.
    public float tempDash;                          // Counter that determinates how much time the player has to be in the DASH state.

    // Animations
    public Animator anim;                                  // The animator component from the player.

    public void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerController = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
    }

    public void Attack10Animation()
    {
        Debug.Log("playing Attack10Animation");
    }
}
