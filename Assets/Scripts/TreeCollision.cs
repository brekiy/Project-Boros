using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollision : MonoBehaviour {



    private void Start()
    {
        
    }

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthScript>())
        {
            other.GetComponent<HealthScript>().DoDamage(5f, 50);
        }
        
    }
}
