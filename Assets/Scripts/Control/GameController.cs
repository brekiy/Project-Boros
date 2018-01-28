using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  public static GameController instance;
  public bool gameOver = false;
  public int deaths = 0;

  public bool paused = false;
  public string curBody;
  public bool earthDefeated = false;

  void Awake() {
    if (instance == null) instance = this;
    else if (instance != this) Destroy(gameObject);

  }

  // Use this for initialization
  void Start () {
    if (!gameOver && Input.GetKeyDown(KeyCode.P)) {
      if (!paused) {
        paused = true;
        Time.timeScale = 0;
      } else {
        paused = false;
        Time.timeScale = 1;
      }
    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
