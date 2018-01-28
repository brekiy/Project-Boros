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
    BlancheController player = GameObject.FindGameObjectWithTag("Blanche").GetComponent<BlancheController>();
        if (playerHealth < 0) player.playerState = BlancheController.PlayerState.dead;
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
