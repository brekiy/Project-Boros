using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStamDisplay : MonoBehaviour {

  public int startingHealth;
  float maxStamina = 100f;
  public int startingStamina;
  public float staminaRecover = 1f;
  public float playerHealth; //current player hp
  public float playerStamina;
  public Slider healthSlider;
  public Slider staminaSlider;
  bool stamRegen;
  public bool dead;

	// Use this for initialization
	void Start () {
    playerHealth = startingHealth;
    playerStamina = startingStamina;
    healthSlider.value = startingHealth;
    staminaSlider.value = startingStamina;
  }
	
	// Update is called once per frame
	void Update () {
    if(!stamRegen && playerStamina < maxStamina) StartCoroutine(StaminaRegen());
  }

  public void TakeDamage(int amount) {
    playerHealth -= amount;
    healthSlider.value = playerHealth;
    if (playerHealth < 0) dead = true;
  }

  private IEnumerator StaminaRegen() {
    stamRegen = true;
    if (playerStamina < maxStamina) {
      playerStamina += staminaRecover;
      staminaSlider.value = playerStamina;
      yield return new WaitForSeconds(0.2f);
    }
    stamRegen = false;
  }
}
