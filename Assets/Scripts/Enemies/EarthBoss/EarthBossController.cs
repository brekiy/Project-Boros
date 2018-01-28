using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarthBossController : MonoBehaviour {

  NavMeshAgent nav;
  Vector3 playerPos;
  Vector3 selfPos;
  public int bossHealth;
  private bool lowHealth;
  Animator anim;
  Rigidbody rigid;


  //movement vars
  public float chaseSpeed;
  public float walkSpeed;
  private float attackingSpeed = 0.01f;
  public float chaseDist;

  //attack/range vars
  public float maxAtkRange;
  public float medAtkRange;
  public float closeAtkRange;

  enum aiState { walking, chasing, maxAtk, medAtk, closeAtk, dead}
  aiState curState;
  

	// Use this for initialization
	void Awake () {
    anim = GetComponent<Animator>();
    nav = GetComponent<NavMeshAgent>();
    rigid = GetComponent<Rigidbody>();
    playerPos = GameObject.Find("Player").transform.position;
    selfPos = nav.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
    playerPos = GameObject.Find("Player").transform.position;
    selfPos = nav.transform.position;
    nav.SetDestination(playerPos);
    
    Debug.Log(curState);
    StateLogic();
    StartCoroutine(StateMachine());
  }

  private void StateLogic() {
    var distance = Vector3.Distance(selfPos, playerPos);
    anim.SetInteger("ActionIndex", 0);
    if (distance >= chaseDist) {
      //rush at the player untl we walk
      curState = aiState.chasing;
      anim.SetBool("Action", true);
      anim.SetInteger("ActionIndex", 5);
      anim.SetBool("Moving", false);
    }
    else if (distance < chaseDist && distance >= maxAtkRange) {
      //walk until we're in range to max range atk
      curState = aiState.walking;
      anim.SetBool("Action", false);
      anim.SetBool("Moving", true);
    }
    else if (distance < maxAtkRange && distance >= medAtkRange) //do the max range atk
      curState = aiState.maxAtk;
    else if (distance < medAtkRange && distance >= closeAtkRange) //do the med range atks
      curState = aiState.medAtk;
    else if (distance < closeAtkRange) //do the close range atk
      curState = aiState.closeAtk;
    else anim.SetBool("Action", false);
    if (bossHealth <= 0) curState = aiState.dead;
  }

  private IEnumerator StateMachine() {
    bool notDead = true;
    while (notDead) {
      switch (curState) {
        case aiState.chasing:
          Chase();
          break;
        case aiState.walking:
          Walk();
          break;
        case aiState.maxAtk:
          MaxAttack();
          break;
        case aiState.medAtk:
          MedAttack();
          break;
        case aiState.closeAtk:
          CloseAttack();
          break;
        case aiState.dead:
          notDead = false;
          Dead();
          break;
      }
      yield return new WaitForSeconds(4);
    }
    Debug.Log("Earth boss is very dead");
  }

  private void Chase() {
    nav.speed = chaseSpeed;
    
  }

  private void Walk() {
    nav.speed = walkSpeed;
  }

  private void MaxAttack() {
    nav.speed = attackingSpeed;
    anim.SetBool("Moving", false);
    anim.SetBool("Action", true);
    anim.SetInteger("ActionIndex", 2);
  }

  private void MedAttack() {
    nav.speed = attackingSpeed;
    anim.SetBool("Moving", false);
    anim.SetBool("Action", true);
    if (Random.Range(0, 5) < 4) anim.SetInteger("ActionIndex", 3);
    else anim.SetInteger("ActionIndex", 7);
  }

  private void CloseAttack() {
    nav.speed = attackingSpeed;
    anim.SetBool("Moving", false);
    anim.SetBool("Action", true);
  }

  private void Dead() {

  }
}
