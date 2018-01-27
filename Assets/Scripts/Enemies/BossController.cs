using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//THIS IS A TEMPLATE SCRIPT FOR THE TEST BOSS
public class BossController : MonoBehaviour {

  NavMeshAgent nav;
  Transform playerPos;
  public int BossHealth;


	// Use this for initialization
	void Awake () {
    nav = GetComponent<NavMeshAgent>();
    playerPos = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
    nav.SetDestination(playerPos.position);
	}


}
