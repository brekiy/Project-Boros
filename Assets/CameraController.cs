using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        var cx = Input.GetAxis("CamHorizontal") * Time.deltaTime * 150.0f;
        
        transform.Rotate(cx, 0, 0);
    }
}
