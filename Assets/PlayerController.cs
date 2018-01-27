using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float playerSpeed = 5;
    float cameraSpeed = 150;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        var cy = Input.GetAxis("CamHorizontal") * Time.deltaTime * cameraSpeed;
        
        transform.Translate(x, 0, z);
        transform.Rotate(0, cy, 0);
    }
}
