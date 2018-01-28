using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaScript : MonoBehaviour {

    float entityMaxStamina = 0;
    float entityStamina = 0;
    float staminaRegen = 0;
    float regenDelay = 0;

    // Use this for initialization

    public float GetStamina()
    {
        return entityStamina;
    }

    public float GetMaxStamina()
    {
        return entityMaxStamina;
    }

    public void SetStamina(float stamina, float maxStamina)
    {
        entityStamina = stamina;
        entityMaxStamina = maxStamina;
    }

    public void SetRegen(float regen)
    {
        staminaRegen = regen;
    }

    public void UseStamina(float amount,float delay)
    {
        entityStamina = Mathf.Max(entityStamina - amount, 0);
        regenDelay = delay;
    }
    
    private void Update()
    {
        regenDelay -= 60*Time.deltaTime;
        regenDelay = Mathf.Max(regenDelay, 0);
        if (regenDelay == 0)
        {
            entityStamina = Mathf.Min(entityStamina + staminaRegen * 60*Time.deltaTime,entityMaxStamina);
        }
    }
}
