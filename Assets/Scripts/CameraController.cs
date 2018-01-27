using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector3 v3Rotate = Vector3.zero;
    int minAngle = -25;
    int maxAngle = 25;

    float cameraSpeed = 150;

    // Use this for initialization
    void Start()
    {
        transform.localEulerAngles = v3Rotate;
    }

    // Update is called once per frame
    void Update()
    {
        var cx = Input.GetAxis("CamVertical") * Time.deltaTime * cameraSpeed;
        v3Rotate.x += cx;
        v3Rotate.x = Mathf.Clamp(v3Rotate.x, minAngle, maxAngle);

        transform.localEulerAngles = v3Rotate;
    }
}
