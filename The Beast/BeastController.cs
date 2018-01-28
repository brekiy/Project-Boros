using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastController : MonoBehaviour {

	Animator anim;


	void Start()
	{
		anim = this.GetComponent<Animator>();

	}

	void Update()
	{
		if (Input.GetButtonDown("Horizontal")){
			anim.SetBool("Walking", true);
		}
	}
}
