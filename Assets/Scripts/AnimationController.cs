using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = playerController.animator;
    }

    private void SetSpriteSide()
    {
        if (playerController.inputBrain.horAxis < 0)
        {
            playerController.spRenderer.flipX = true;
        }
        else if (playerController.inputBrain.horAxis > 0)
        {
            playerController.spRenderer.flipX = false;
        }
    }

    private void Update()
    {
       SetSpriteSide();
       
       animator.SetBool("Jump", playerController.isJumping);
       animator.SetBool("Run", playerController.inputBrain.horAxis != 0 && playerController.isGrounded);
       animator.SetBool("Falling", playerController.isFalling);
       animator.SetBool("OnGround", playerController.isGrounded);
       animator.SetBool("Block", playerController.inputBrain.blocking);
       
       if (playerController.isFalling && playerController.isGrounded)
       {
           animator.SetTrigger("Landed");
       }

       if (playerController.inputBrain.rolling && playerController.canRoll)
       {
           animator.SetTrigger("Roll");
       }
       
       
    }
}
