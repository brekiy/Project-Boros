using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyScript : MonoBehaviour {

    Rigidbody rb;
    Vector3 playerVelocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        playerVelocity = rb.velocity;
        playerVelocity.y = 0f;
        playerVelocity -= Vector3.ClampMagnitude(playerVelocity, 0.5f);
        playerVelocity.y = rb.velocity.y;
        rb.velocity = playerVelocity;
        rb.AddForce(0,-10f,0);
    }
}
