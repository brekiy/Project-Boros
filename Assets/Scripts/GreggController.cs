using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreggController : MonoBehaviour {

    // Animations

    Vector3 playerPos;
    Vector3 selfPos;
    public int bossHealth;
    private bool lowHealth;
    Animator anim;
    Rigidbody rigid;


    //movement vars
    public float chaseSpeed;
    public float walkSpeed;
    private float attackingSpeed = 0.01f;
    public float chaseDist;

    //attack/range vars
    public float maxAtkRange;
    public float medAtkRange;
    public float closeAtkRange;

    enum playerState { walking, chasing, maxAtk, medAtk, closeAtk, dead, idle }
    playerState currentState;

    int delayMovement = 0;
    int delayLook = 0;
    int delayDM = 0;
    int delayDL = 0;

    //Physics

    private float playerMaxSpeed = 8;
    private float cameraSpeed = 150;

    private float playerAccel = 1f;

    private float gravityStrength = -10f;
    Vector3 gravity = Vector3.zero;

    public Vector3 playerVelocity = Vector3.zero;
    Vector3 moveDir = Vector3.zero;

    Vector3 v3Rotate = Vector3.zero;

    Rigidbody rb;

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

    // Update is called once per frame
    void Update()
    {

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = transform.TransformDirection(moveDir);
        moveDir = Vector3.ClampMagnitude(moveDir, 1);

        playerVelocity.y = 0f;

        if (delayMovement < 1)
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
            delayDM -= 1;
            delayDM = Mathf.Max(delayDM, 0);
        }
        else
        {
            delayMovement -= 1;
        }

        if (IsGrounded())
        {
            playerVelocity -= Vector3.ClampMagnitude(playerVelocity, playerAccel * 0.5f);

            if (currentState == playerState.idle | currentState == playerState.walking | delayDM > 0)
            {
                if (currentState == playerState.walking | delayDM > 0)
                {
                    playerVelocity += (playerAccel * moveDir);
                    playerVelocity = Vector3.ClampMagnitude(playerVelocity, playerMaxSpeed);
                }

                if (Input.GetButtonDown("Special"))
                {
                    anim.SetBool("Moving", false);
                    anim.SetBool("Action", true);
                    anim.SetInteger("ActionIndex", 2);
                    currentState = playerState.maxAtk;
                    delayMovement = 80;
                    delayLook = 70;
                    delayDM = 30;
                    delayDL = 60;
                }
            }
            playerVelocity.y = rb.velocity.y;
        }
        else
        {
            playerVelocity.y = rb.velocity.y;
        }

        playerVelocity.y = rb.velocity.y;

        rb.velocity = (playerVelocity);
        gravity.y = gravityStrength;
        rb.AddForce(gravity);

        var cy = Input.GetAxis("CamHorizontal") * Time.deltaTime * cameraSpeed;
        if (delayLook < 1 | delayDL > 0)
        {
            delayDL -= 1;
            delayDL = Mathf.Max(delayDL, 0);
            v3Rotate.y += cy;
        }
        else
        {
            delayLook -= 1;
        }

        transform.localEulerAngles = v3Rotate;
    }
}
