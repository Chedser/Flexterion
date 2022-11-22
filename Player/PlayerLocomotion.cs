using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    Animator anim;
    PlayerMove playerMove;
    Health health;

    float inputX;
    float inputY;

  public  float mouseX;
  public float mouseY;

  public float mouseXTol = 0.1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponentInParent<PlayerMove>();
        health = GetComponentInParent<Health>();
    }

    private void Update()
    {

        if (health.CurrentHealth <= 0) { return; }

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        anim.SetBool("isGrounded", playerMove.IsGrounded);

    /*    if (playerMove.IsGrounded && inputX == 0 && inputY == 0 && Mathf.Abs(mouseX) > mouseXTol)
        {

            anim.SetBool("isTurning", true);

        }
        else {

            anim.SetBool("isTurning", false);

        } */
        
        anim.SetFloat("inputX", inputX);
        anim.SetFloat("inputY", inputY);
    
    }

}
