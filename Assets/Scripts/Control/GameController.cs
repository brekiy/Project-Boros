using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  public static GameController instance;
  public bool gameOver = false;
  public int deaths = 0;

  public bool paused;
  public string curBody;

  void Awake() {
    if (instance == null) instance = this;
    else if (instance != this) Destroy(gameObject);

  }

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
