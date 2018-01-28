using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreggController : MonoBehaviour {

    // Animations

    public int bossHealth;
    private bool lowHealth;
    Animator anim;
    Rigidbody rigid;

    enum playerState { walking, running, maxAtk, medAtk, closeAtk, dead, idle, shielding, firstAtk, secondAtk, spin }
    playerState currentState;

    enum buttonState { none, Left, Right, LeftHeavy, RightHeavy, MovementAction, Special, AOE }
    buttonState inputBuffer;

    float delayMovement = 0;
    float delayLook = 0;
    float delayDM = 0;
    float delayDL = 0;

    float spinTime = 0;

    //Physics

    private float playerMaxSpeed = 7;
    private float cameraSpeed = 150;

    private float playerAccel = 0.4f;

    private float gravityStrength = -10f;
    Vector3 gravity = Vector3.zero;

    public Vector3 playerVelocity = Vector3.zero;
    Vector3 moveDir = Vector3.zero;

    Vector3 v3Rotate = Vector3.zero;

    Rigidbody rb;

    HealthScript health;
    StaminaScript stamina;

    // Use this for initialization
    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentState = playerState.idle;
        anim.SetBool("Action", false);
        anim.SetInteger("ActionIndex", 0);
        anim.SetBool("Moving", false);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<HealthScript>();
        stamina = GetComponent<StaminaScript>();

        health.SetHealth(200, 200);
        stamina.SetStamina(100, 100);
        stamina.SetRegen(50 * Time.deltaTime);
    }

    void SetPlayerSpeed(float speed, float accel)
    {
        playerMaxSpeed = speed;
        playerAccel = accel;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.5f);
    }

    void InputBuffer()
    {
        if (Input.GetButtonDown("AOE")) inputBuffer = buttonState.AOE;
        if (Input.GetButtonDown("Special")) inputBuffer = buttonState.Special;
        if (Input.GetButtonDown("MovementAction")) inputBuffer = buttonState.MovementAction;
        if (Input.GetButtonDown("Left")) inputBuffer = buttonState.Left;
        if (Input.GetAxis("Heavy") > 0.1) inputBuffer = buttonState.LeftHeavy;
        if (Input.GetButtonDown("Right")) inputBuffer = buttonState.Right;
        if (Input.GetAxis("Heavy") < -0.1) inputBuffer = buttonState.RightHeavy;
    }


    // Update is called once per frame
    void Update()
    {

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = transform.TransformDirection(moveDir);
        moveDir = Vector3.ClampMagnitude(moveDir, 1);

        playerVelocity.y = 0f;

        if (currentState == playerState.idle | currentState == playerState.walking)
        {
            inputBuffer = buttonState.none;
        }
        else
        {
            InputBuffer();
        }

        if (currentState == playerState.shielding)
        {
            stamina.UseStamina(10 * Time.deltaTime, 120);
            if (stamina.GetStamina() == 0)
            {
                currentState = playerState.idle;
            }
        }

        if (delayMovement < 1 && (currentState != playerState.shielding | Input.GetButtonUp("Left")))
        {
            if (moveDir.magnitude > 0.05f && IsGrounded())
            {
                currentState = playerState.walking;
                anim.SetBool("Moving", true);
            }
            else
            {
                currentState = playerState.idle;
                anim.SetBool("Moving", false);
            }
            anim.SetBool("Action", false);
        }
        else if (delayDM > 0)
        {
            delayDM -= 60 * Time.deltaTime;
            delayDM = Mathf.Max(delayDM, 0);
        }
        else
        {
            delayMovement -= 60 * Time.deltaTime;
        }

        if (IsGrounded())
        {
            playerVelocity -= Vector3.ClampMagnitude(playerVelocity, playerAccel * 0.75f);

            if ((currentState == playerState.firstAtk) && (inputBuffer == buttonState.Right) && delayMovement > 0)
            {
                if (delayMovement < 90 && stamina.GetStamina() >= 20)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 10);
                    currentState = playerState.secondAtk;
                    delayMovement += 70;
                    delayLook += 68;
                    stamina.UseStamina(20,120);
                }
                else
                {
                    inputBuffer = buttonState.none;
                }
                
            }

            if (currentState == playerState.idle | currentState == playerState.walking | currentState == playerState.running | delayDM > 0)
            {
                if (currentState == playerState.walking | currentState == playerState.running | delayDM > 0)
                {
                    playerVelocity += (playerAccel * moveDir);
                    if (currentState != playerState.running)
                    {
                        playerVelocity = Vector3.ClampMagnitude(playerVelocity, playerMaxSpeed);
                    }
                    else
                    {
                        playerVelocity = playerVelocity + (transform.forward * 0.6f);
                        playerVelocity = Vector3.ClampMagnitude(playerVelocity, playerMaxSpeed*1.5f);
                    }
                }

                if (Input.GetAxis("Heavy") > 0.1 && stamina.GetStamina() >= 30)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 2);
                    currentState = playerState.maxAtk;
                    delayMovement = 80;
                    delayLook = 70;
                    delayDM = 30;
                    delayDL = 60;
                    stamina.UseStamina(30, 120);
                }

                if (Input.GetAxis("Heavy") < -0.1 && stamina.GetStamina() >= 15)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 4);
                    currentState = playerState.maxAtk;
                    delayMovement = 80;
                    delayLook = 70;
                    delayDM = 30;
                    delayDL = 60;
                    stamina.UseStamina(15, 120);
                }

                if (Input.GetButtonDown("MovementAction") && stamina.GetStamina() >= 15)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 5);
                    currentState = playerState.running;
                    delayMovement = 55;
                    delayDM = 5;
                    stamina.UseStamina(15, 120);
                }

                if (Input.GetButtonDown("Right") && stamina.GetStamina() >= 25)
                {
                    if (currentState != playerState.firstAtk)
                    {
                        anim.SetBool("Moving", false);
                        anim.SetBool("Action", true);
                        anim.SetInteger("ActionIndex", 3);
                        currentState = playerState.firstAtk;
                        delayMovement = 144;
                        delayLook = 75;
                        delayDM = 5;
                        delayDL = 75;
                        stamina.UseStamina(25, 120);
                    }
                    
                }

                if (Input.GetButtonDown("AOE") && stamina.GetStamina() >= 35)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 7);
                    currentState = playerState.spin;
                    spinTime = 420;
                    delayMovement = 165;
                    delayLook = 240;
                    delayDM = 15;
                    delayDL = 180;
                    stamina.UseStamina(35, 120);
                }

                if (Input.GetButtonDown("Special") && stamina.GetStamina() >= 20)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 6);
                    currentState = playerState.maxAtk;
                    delayMovement = 80;
                    delayLook = 70;
                    delayDM = 30;
                    delayDL = 60;
                    stamina.UseStamina(20, 120);
                }

                if (Input.GetButtonDown("Left") && stamina.GetStamina() >= 10)
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 1);
                    currentState = playerState.shielding;
                    stamina.UseStamina(10, 120);
                }

            }
        }

        if (spinTime > 0)
        {
            spinTime -= 60*Time.deltaTime;
            playerVelocity = playerVelocity * 0.95f;

        }
        playerVelocity.y = rb.velocity.y;
        rb.velocity = (playerVelocity);
        gravity.y = gravityStrength;
        rb.AddForce(gravity);

        var cy = Input.GetAxis("CamHorizontal") * Time.deltaTime * cameraSpeed;
        if (delayLook < 1 | delayDL > 0)
        {
            delayDL -= 60 * Time.deltaTime;
            delayDL = Mathf.Max(delayDL, 0);
            v3Rotate.y += cy;
        }
        else
        {
            delayLook -= 60 * Time.deltaTime;
        }

        transform.localEulerAngles = v3Rotate;
    }
}
