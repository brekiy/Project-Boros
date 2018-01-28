using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  public static GameController instance;
  public bool gameOver = false;

  public bool paused = false;
  public bool earthDefeated = false;

  public HealthStamDisplay healthDisplay;

  void Awake() {
    if (instance == null) instance = this;
    else if (instance != this) Destroy(gameObject);
  }

  // Use this for initialization
  void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
    //if (healthDisplay.playerHealth < 0) gameOver = true;
    if (!gameOver && Input.GetKeyDown(KeyCode.P)) {
      if (!paused) {
        paused = true;
        Time.timeScale = 0;
      }
      else {
        paused = false;
        Time.timeScale = 1;
      }
    }
  }
}
