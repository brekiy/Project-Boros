using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    float entityMaxHealth = 0;
    float entityHealth = 0;
    int invulFrames = 0;

    // Use this for initialization

    public float GetHealth()
    {
        return entityHealth;
    }

    public float GetMaxHealth()
    {
        return entityMaxHealth;
    }

    public void SetHealth(float health, float maxHealth)
    {
        entityHealth = health;
        entityMaxHealth = maxHealth;
    }

    public void DoDamage(float amount, int delay)
    {
        if (invulFrames < 1)
        {
            entityHealth = Mathf.Max(entityHealth - amount, 0);
            invulFrames = delay;
        }
        Debug.Log(entityHealth);
    }
    
    private void Update()
    {
        invulFrames -= 1;
        invulFrames = Mathf.Max(invulFrames,0);
    }
}
