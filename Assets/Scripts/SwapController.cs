using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapController : MonoBehaviour {

    string currentBody = "Blanche";

    GameObject gregg;
    GameObject blanche;

    // Use this for initialization
    void Start () {
        gregg = GameObject.FindGameObjectWithTag("Gregg");
        blanche = GameObject.FindGameObjectWithTag("Blanche");
        gregg.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire2"))
        {
            if (currentBody == "Blanche")
            {
                currentBody = "Gregg";
                gregg.SetActive(true);
                blanche.SetActive(false);
            }
            else
            {
                currentBody = "Blanche";
                gregg.SetActive(false);
                blanche.SetActive(true);
            }
        }
	}
}
