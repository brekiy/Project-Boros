using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour {

    HealthScript health;

    // Use this for initialization
    void Awake () {
        health.SetHealth(1000,1000);
	}
	
	// Update is called once per frame
	void Update () {
        if (health.GetHealth() != 1000)
        {
            transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<HealthScript>().DoDamage(1000 - health.GetHealth(), health.invulFrames);
            health.SetHealth(1000,1000);
        }

    }
}
