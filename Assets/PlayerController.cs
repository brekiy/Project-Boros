using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float playerMaxSpeed = 10;
    private float cameraSpeed = 150;

    private float playerAccel = 1f;

    private float gravityStrength = -10f;
    Vector3 gravity = Vector3.zero;

    private float jumpStrength = 100f;

    Vector3 playerVelocity = Vector3.zero;
    Vector3 moveDir = Vector3.zero;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = transform.TransformDirection(moveDir);
        moveDir = Vector3.ClampMagnitude(moveDir, 1);

        playerVelocity.x = playerVelocity.x - (0.5f * playerAccel * Mathf.Sign(playerVelocity.x));
        playerVelocity.z = playerVelocity.z - (0.5f * playerAccel * Mathf.Sign(playerVelocity.z));

        playerVelocity.y = rb.velocity.y;

        playerVelocity = playerVelocity + (playerAccel * moveDir);
        playerVelocity = Vector3.ClampMagnitude(playerVelocity, playerMaxSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += jumpStrength;
        }

        rb.velocity = (playerVelocity);
        gravity.y = gravityStrength;
        rb.AddForce(gravity);

        var cy = Input.GetAxis("CamHorizontal") * Time.deltaTime * cameraSpeed;
        
        transform.Rotate(0, cy, 0);
    }
}
