﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlancheController : MonoBehaviour {

   

    HealthStamDisplay playerHealthStam;
    
    //Physics

    private float playerMaxSpeed = 10;
    private float cameraSpeed = 150;

    private float playerAccel = 1f;

    private float gravityStrength = -10f;
    Vector3 gravity = Vector3.zero;

    private float jumpStrength = 9f;
    public float jumpCost;
    public float attackCost;

    public Vector3 playerVelocity = Vector3.zero;
    Vector3 moveDir = Vector3.zero;

    Vector3 v3Rotate = Vector3.zero;

    Rigidbody rb;
    
    //State
    public enum PlayerState {idle, lightAttack, heavyAttack, dead}
    public PlayerState playerState;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHealthStam = GetComponent<HealthStamDisplay>();
        playerState = PlayerState.idle;
    }

    void SetPlayerSpeed(float speed, float accel)
    {
        playerMaxSpeed = speed;
        playerAccel = accel;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = transform.TransformDirection(moveDir);
        moveDir = Vector3.ClampMagnitude(moveDir, 1);

        playerVelocity.y = 0f;

        if (IsGrounded())
        {
            playerVelocity -= Vector3.ClampMagnitude(playerVelocity, playerAccel * 0.5f);
            playerVelocity += (playerAccel * moveDir);
            playerVelocity = Vector3.ClampMagnitude(playerVelocity, playerMaxSpeed);
            playerVelocity.y = rb.velocity.y;
            if (playerState == PlayerState.idle)
            {
                if (Input.GetButtonDown("MovementAction") && playerHealthStam.playerStamina >= jumpCost)
                {
                    playerHealthStam.playerStamina -= jumpCost;
                    playerVelocity.y += jumpStrength;
                }
                else if (Input.GetButtonDown("Special") && playerHealthStam.playerStamina >= attackCost)
                {
                    playerState = PlayerState.lightAttack;
                    playerHealthStam.playerStamina -= attackCost;
                }
                    
            }
            if (playerHealthStam.playerHealth <= 0) playerState = PlayerState.dead;
        }
        else
        {
            playerVelocity.y = rb.velocity.y;
        }

        rb.velocity = (playerVelocity);
        gravity.y = gravityStrength;
        rb.AddForce(gravity);

        var cy = Input.GetAxis("CamHorizontal") * Time.deltaTime * cameraSpeed;
        v3Rotate.y += cy;

        transform.localEulerAngles = v3Rotate;
    }

    void Dead()
    {
        GameController instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        instance.gameOver = true;
    }
}
