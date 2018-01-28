using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStamDisplay : MonoBehaviour {

  public int startingHealth;
  int maxStamina = 100;
  public int startingStamina;
  public float staminaRecover = 1f;
  [HideInInspector] public float playerHealth; //current player hp
  [HideInInspector] public float playerStamina;
  public Slider healthSlider;
  public Slider staminaSlider;
  public bool dead;

	// Use this for initialization
	void Start () {
    playerHealth = startingHealth;
    playerStamina = startingStamina;
    healthSlider.value = startingHealth;
    staminaSlider.value = startingStamina;
    StartCoroutine(StaminaRegen());
  }
	
	// Update is called once per frame
	void Update () {
    
	}

  public void TakeDamage(int amount) {
    playerHealth -= amount;
    healthSlider.value = playerHealth;
    if (playerHealth < 0) dead = true;
  }

  private IEnumerator StaminaRegen() {
    if (playerStamina < maxStamina) {
      playerStamina += staminaRecover;
      staminaSlider.value = playerStamina;
      yield return new WaitForSeconds(4);
    }
    
  }
}
