using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollision : MonoBehaviour {

    public float damageAmount;

    public void Damage(float amount)
    {
        damageAmount = amount;
    }

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (damageAmount > 0 && other.GetComponent<HealthScript>())
        {
            other.GetComponent<HealthScript>().DoDamage(damageAmount, 50);
        }
        
    }
}
