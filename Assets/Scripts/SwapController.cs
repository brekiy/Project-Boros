using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapController : MonoBehaviour {

    string currentBody = "Blanche";

    GameObject gregg;
    GameObject blanche;
    GameObject greggBody;
    GameObject blancheBody;

    // Use this for initialization
    void Start () {
        gregg = GameObject.FindGameObjectWithTag("Gregg");
        blanche = GameObject.FindGameObjectWithTag("Blanche");
        greggBody = GameObject.FindGameObjectWithTag("GreggBody");
        blancheBody = GameObject.FindGameObjectWithTag("BlancheBody");
        gregg.SetActive(false);
        blancheBody.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Interact"))
        {
            if (currentBody == "Blanche")
            {
                if ((blanche.transform.position - greggBody.transform.position).magnitude < 2)
                {
                    currentBody = "Gregg";
                    gregg.SetActive(true);
                    blancheBody.SetActive(true);
                    gregg.transform.position = greggBody.transform.position;
                    gregg.GetComponent<GreggController>().playerVelocity = greggBody.GetComponent<Rigidbody>().velocity;
                    blancheBody.transform.position = blanche.transform.position;
                    blancheBody.GetComponent<Rigidbody>().velocity = blanche.GetComponent<Rigidbody>().velocity;
                    blanche.SetActive(false);
                    greggBody.SetActive(false);
                }
            }
            else
            {
                if ((gregg.transform.position - blancheBody.transform.position).magnitude < 2)
                {
                    currentBody = "Blanche";
                    blanche.SetActive(true);
                    greggBody.SetActive(true);
                    blanche.transform.position = blancheBody.transform.position;
                    blanche.GetComponent<BlancheController>().playerVelocity = blancheBody.GetComponent<Rigidbody>().velocity;
                    greggBody.transform.position = gregg.transform.position;
                    greggBody.GetComponent<Rigidbody>().velocity = gregg.GetComponent<Rigidbody>().velocity;
                    gregg.SetActive(false);
                    blancheBody.SetActive(false);
                }
            }
        }
	}
}
